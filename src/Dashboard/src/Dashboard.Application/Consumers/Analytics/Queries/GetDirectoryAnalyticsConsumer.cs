using Giantnodes.Dashboard.Abstractions.Common;
using Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using System.IO.Abstractions;

namespace Giantnodes.Dashboard.Application.Consumers.Analytics.Queries
{
    public class GetDirectoryAnalyticsConsumer : IConsumer<GetDirectoryAnalytics>
    {
        protected string? CacheKey { get; private set; }
        protected readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(15);

        private readonly IDistributedCache _cache;
        private readonly IFileSystem _system;

        public GetDirectoryAnalyticsConsumer(IDistributedCache cache, IFileSystem system)
        {
            this._cache = cache;
            this._system = system;
        }

        public async Task Consume(ConsumeContext<GetDirectoryAnalytics> context)
        {
            CacheKey = $"analytics:directory:{context.Message.Directory}";

            if (!await _system.Directory.ExistsAsync(context.Message.Directory))
            {
                await context.RejectAsync<GetDirectoryAnalyticsRejected, GetDirectoryAnalyticsRejection>(GetDirectoryAnalyticsRejection.DIRECTORY_NOT_FOUND);
                return;
            }

            var cached = await _cache.GetAsync<GetDirectoryAnalyticsResult>(CacheKey);
            if (cached != null)
            {
                await context.RespondAsync<GetDirectoryAnalyticsResult>(cached);
                return;
            }

            var files = _system.DirectoryInfo
                .FromDirectoryName(context.Message.Directory)
                .GetFiles("*", SearchOption.AllDirectories)
                .Where(file => file.IsMediaFile())
                .ToArray();

            var latest = files
                .OrderByDescending(file => file.LastWriteTimeUtc)
                .Select(file => new FileSystemFile
                {
                    Path = file.FullName,
                    Name = file.Name,
                    DirectoryName = file.DirectoryName,
                    IsReadOnly = file.IsReadOnly,
                    CreatedAtUtc = file.CreationTimeUtc,
                    ModifiedAtUtc = file.LastWriteTimeUtc
                })
                .FirstOrDefault();

            var result = new GetDirectoryAnalyticsResult { LatestModifiedFile = latest, TotalFiles = files.Length, WatchTimeMinutes = 0 };
            await _cache.SetAsync(CacheKey, result, CacheDuration, context.CancellationToken);
            await context.RespondAsync<GetDirectoryAnalyticsResult>(result);
        }
    }
}

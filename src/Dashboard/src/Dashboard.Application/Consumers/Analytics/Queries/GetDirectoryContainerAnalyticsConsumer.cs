using Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using System.IO.Abstractions;

namespace Giantnodes.Dashboard.Application.Consumers.Analytics.Queries
{
    public class GetDirectoryContainerAnalyticsConsumer : IConsumer<GetDirectoryContainerAnalytics>
    {
        protected string? CacheKey { get; private set; }
        protected readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(15);

        private readonly IDistributedCache _cache;
        private readonly IFileSystem _system;

        public GetDirectoryContainerAnalyticsConsumer(IDistributedCache cache, IFileSystem system)
        {
            this._cache = cache;
            this._system = system;
        }

        public async Task Consume(ConsumeContext<GetDirectoryContainerAnalytics> context)
        {
            CacheKey = $"analytics:container:{context.Message.Directory}";

            if (!await _system.Directory.ExistsAsync(context.Message.Directory))
            {
                await context.RejectAsync<GetDirectoryContainerAnalyticsRejected, GetDirectoryContainerAnalyticsRejection>(GetDirectoryContainerAnalyticsRejection.DIRECTORY_NOT_FOUND);
                return;
            }

            var cached = await _cache.GetAsync<DirectoryContainerAnalytics[]>(CacheKey);
            if (cached != null)
            {
                await context.RespondAsync<GetDirectoryContainerAnalyticsResult>(new { Containers = cached });
                return;
            }

            var directory = _system.DirectoryInfo.FromDirectoryName(context.Message.Directory);
            var files = directory.GetFiles("*", SearchOption.AllDirectories)
                .Where(file => file.IsMediaFile())
                .ToArray();

            var total = files.Length;
            var containers = files.GroupBy(file => file.Extension)
                .Select(group => new DirectoryContainerAnalytics
                {
                    Extension = group.Key,
                    TotalFiles = group.Count(),
                    Percent = Math.Round((decimal)group.Count() / total * 100, 2, MidpointRounding.ToEven),
                })
                .ToArray();

            await _cache.SetAsync(CacheKey, containers, CacheDuration, context.CancellationToken);
            await context.RespondAsync<GetDirectoryContainerAnalyticsResult>(new { Containers = containers });
        }
    }
}

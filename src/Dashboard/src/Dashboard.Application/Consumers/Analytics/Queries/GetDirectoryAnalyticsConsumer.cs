using Giantnodes.Dashboard.Abstractions.Common;
using Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries;
using MassTransit;
using System.IO.Abstractions;

namespace Giantnodes.Dashboard.Application.Consumers.Analytics.Queries
{
    public class GetDirectoryAnalyticsConsumer : IConsumer<GetDirectoryAnalytics>
    {
        private readonly IFileSystem _system;

        public GetDirectoryAnalyticsConsumer(IFileSystem system)
        {
            this._system = system;
        }

        public async Task Consume(ConsumeContext<GetDirectoryAnalytics> context)
        {
            if (!_system.Directory.Exists(context.Message.Directory))
            {
                await context.RejectAsync<GetDirectoryAnalyticsRejected, GetDirectoryAnalyticsRejection>(GetDirectoryAnalyticsRejection.DIRECTORY_NOT_FOUND);
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

            await context.RespondAsync<GetDirectoryAnalyticsResult>(new
            {
                LatestModifiedFile = latest,
                TotalFiles = files.Length,
                WatchTimeMinutes = 0
            });
        }
    }
}

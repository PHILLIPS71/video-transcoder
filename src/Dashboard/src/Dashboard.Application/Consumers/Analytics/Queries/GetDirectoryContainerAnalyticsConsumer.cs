using Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries;
using MassTransit;
using System.IO.Abstractions;

namespace Giantnodes.Dashboard.Application.Consumers.Analytics.Queries
{
    public class GetDirectoryContainerAnalyticsConsumer : IConsumer<GetDirectoryContainerAnalytics>
    {
        private readonly IFileSystem _system;

        public GetDirectoryContainerAnalyticsConsumer(IFileSystem system)
        {
            this._system = system;
        }

        public async Task Consume(ConsumeContext<GetDirectoryContainerAnalytics> context)
        {
            if (!_system.Directory.Exists(context.Message.Directory))
            {
                await context.RejectAsync<GetDirectoryContainerAnalyticsRejected, GetDirectoryContainerAnalyticsRejection>(GetDirectoryContainerAnalyticsRejection.DIRECTORY_NOT_FOUND);
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

            await context.RespondAsync<GetDirectoryContainerAnalyticsResult>(new { Containers = containers });
        }
    }
}

using Giantnodes.Dashboard.Abstractions.Features.Statistics.Queries.GetDirectoryContainerStatistics;
using MassTransit;
using System.IO.Abstractions;

namespace Giantnodes.Dashboard.Application.Consumers.Statistics.Queries
{
    public class GetDirectoryContainerStatisticsConsumer : IConsumer<GetDirectoryContainerStatistics>
    {
        private readonly IFileSystem _system;

        public GetDirectoryContainerStatisticsConsumer(IFileSystem system)
        {
            this._system = system;
        }

        public async Task Consume(ConsumeContext<GetDirectoryContainerStatistics> context)
        {
            if (!_system.Directory.Exists(context.Message.Directory))
            {
                await context.RejectAsync<GetDirectoryContainerStatisticsRejected, GetDirectoryContainerStatisticsRejection>(GetDirectoryContainerStatisticsRejection.DIRECTORY_NOT_FOUND);
                return;
            }

            var directory = _system.DirectoryInfo.FromDirectoryName(context.Message.Directory);
            var files = directory.GetFiles("*", SearchOption.AllDirectories)
                .Where(file => file.IsMediaFile())
                .ToArray();

            var total = files.Length;
            var containers = files.GroupBy(file => file.Extension)
                .Select(group => new DirectoryContainerStatistic
                {
                    Extension = group.Key,
                    Percent = Math.Round((decimal)group.Count() / total * 100, 2, MidpointRounding.ToEven)
                })
                .ToArray();

            await context.RespondAsync<GetDirectoryContainerStatisticsResult>(new { Containers = containers });
        }
    }
}

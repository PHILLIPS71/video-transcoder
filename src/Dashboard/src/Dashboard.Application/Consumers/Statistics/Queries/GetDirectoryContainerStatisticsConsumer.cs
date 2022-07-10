using Giantnodes.Dashboard.Abstractions.Features.FileExplorer;
using Giantnodes.Dashboard.Abstractions.Features.Statistics.Queries.GetDirectoryContainerStatistics;
using MassTransit;

namespace Giantnodes.Dashboard.Application.Consumers.Statistics.Queries
{
    public class GetDirectoryContainerStatisticsConsumer : IConsumer<GetDirectoryContainerStatistics>
    {
        public async Task Consume(ConsumeContext<GetDirectoryContainerStatistics> context)
        {
            if (!Directory.Exists(context.Message.Directory))
            {
                await context.RejectAsync<GetDirectoryContainerStatisticsRejected, GetDirectoryContainerStatisticsRejection>(GetDirectoryContainerStatisticsRejection.DIRECTORY_NOT_FOUND);
                return;
            }

            var directory = new DirectoryInfo(context.Message.Directory);
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

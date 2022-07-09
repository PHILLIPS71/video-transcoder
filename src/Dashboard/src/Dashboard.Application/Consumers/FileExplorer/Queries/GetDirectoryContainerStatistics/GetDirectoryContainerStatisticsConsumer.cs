using Giantnodes.Dashboard.Abstractions.Features.FileExplorer;
using Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Queries;
using Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Results;
using MassTransit;

namespace Giantnodes.Dashboard.Application.Consumers.FileExplorer
{
    public class GetDirectoryContainerStatisticsConsumer : IConsumer<GetDirectoryContainerStatisticsQuery>
    {
        public async Task Consume(ConsumeContext<GetDirectoryContainerStatisticsQuery> context)
        {
            if (!Directory.Exists(context.Message.Directory))
                return;

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

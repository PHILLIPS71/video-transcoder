using Giantnodes.Dashboard.Abstractions.Features.FileExplorer;
using Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Queries;
using Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Results;
using MassTransit;

namespace Giantnodes.Dashboard.Api.FileSystem
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class GetDirectoryContainerStatistics
    {
        [UseSorting]
        public async Task<IEnumerable<DirectoryContainerStatistic>> DirectoryContainerStatistics(
            [Service] IRequestClient<GetDirectoryContainerStatisticsQuery> client,
            GetDirectoryContainerStatisticsQuery input,
            CancellationToken cancellation
        )
        {
            Response response = await client.GetResponse<GetDirectoryContainerStatisticsResult>(input, cancellation);
            return response switch
            {
                (_, GetDirectoryContainerStatisticsResult result) => result.Containers.AsEnumerable(),
                _ => throw new InvalidOperationException()
            };
        }
    }
}

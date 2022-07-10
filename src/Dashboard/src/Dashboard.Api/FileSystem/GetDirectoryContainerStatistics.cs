using Giantnodes.Application.Validation;
using Giantnodes.Dashboard.Abstractions.Features.Statistics.Queries.GetDirectoryContainerStatistics;
using Giantnodes.Infrastructure.Exceptions;
using MassTransit;

namespace Giantnodes.Dashboard.Api.FileSystem
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class GetDirectoryContainerStatisticsResolver
    {
        [UseSorting]
        public async Task<IEnumerable<DirectoryContainerStatistic>> DirectoryContainerStatistics(
            [Service] IRequestClient<GetDirectoryContainerStatistics> client,
            GetDirectoryContainerStatistics input,
            CancellationToken cancellation
        )
        {
            Response response = await client.GetResponse<GetDirectoryContainerStatisticsResult, ValidationFault, GetDirectoryContainerStatisticsRejected>(input, cancellation);
            return response switch
            {
                (_, GetDirectoryContainerStatisticsResult result) => result.Containers.AsEnumerable(),
                (_, ValidationFault error) => throw new DomainException<ValidationFault>(error),
                (_, GetDirectoryContainerStatisticsRejected error) => throw new DomainException<GetDirectoryContainerStatisticsRejected>(error),
                _ => throw new InvalidOperationException()
            };
        }
    }
}

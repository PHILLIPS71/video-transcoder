using Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries;
using Giantnodes.Infrastructure.Exceptions;
using Giantnodes.Infrastructure.Masstransit.Validation;
using MassTransit;

namespace Giantnodes.Dashboard.Api.Resolvers
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class GetDirectoryContainerAnalyticsResolver
    {
        [UseSorting]
        public async Task<IEnumerable<DirectoryContainerAnalytics>> DirectoryContainerAnalytics(
            [Service] IRequestClient<GetDirectoryContainerAnalytics> client,
            GetDirectoryContainerAnalytics input,
            CancellationToken cancellation
        )
        {
            Response response = await client.GetResponse<GetDirectoryContainerAnalyticsResult, GetDirectoryContainerAnalyticsRejected, ValidationFault>(input, cancellation);
            return response switch
            {
                (_, GetDirectoryContainerAnalyticsResult result) => result.Containers,
                (_, GetDirectoryContainerAnalyticsRejected error) => throw new DomainException<GetDirectoryContainerAnalyticsRejected>(error),
                (_, ValidationFault error) => throw new DomainException<ValidationFault>(error),
                _ => throw new InvalidOperationException()
            };
        }
    }
}

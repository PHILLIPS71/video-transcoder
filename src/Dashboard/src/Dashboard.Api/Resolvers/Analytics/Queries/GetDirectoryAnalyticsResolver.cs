using Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries;
using Giantnodes.Infrastructure.Exceptions;
using Giantnodes.Infrastructure.Masstransit.Validation;
using MassTransit;

namespace Giantnodes.Dashboard.Api.Resolvers
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class GetDirectoryAnalyticsResolver
    {
        public async Task<GetDirectoryAnalyticsResult> DirectoryAnalytics(
            [Service] IRequestClient<GetDirectoryAnalytics> client,
            GetDirectoryAnalytics input,
            CancellationToken cancellation
        )
        {
            Response response = await client.GetResponse<GetDirectoryAnalyticsResult, GetDirectoryAnalyticsRejected, ValidationFault>(input, cancellation);
            return response switch
            {
                (_, GetDirectoryAnalyticsResult result) => result,
                (_, GetDirectoryAnalyticsRejected error) => throw new DomainException<GetDirectoryAnalyticsRejected>(error),
                (_, ValidationFault error) => throw new DomainException<ValidationFault>(error),
                _ => throw new InvalidOperationException()
            };
        }
    }
}

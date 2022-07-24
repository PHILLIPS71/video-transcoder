using Giantnodes.Infrastructure.Contracts;

namespace Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries
{
    public record GetDirectoryContainerAnalyticsRejected : IRejected<GetDirectoryContainerAnalyticsRejection>
    {
        public Guid ConversationId { get; init; }

        public DateTime TimeStamp { get; init; }

        public GetDirectoryContainerAnalyticsRejection ErrorCode { get; init; }

        public string Reason { get; init; } = null!;
    }
}

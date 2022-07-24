using Giantnodes.Infrastructure.Contracts;

namespace Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries
{
    public record GetDirectoryAnalyticsRejected : IRejected<GetDirectoryAnalyticsRejection>
    {
        public Guid ConversationId { get; init; }

        public DateTime TimeStamp { get; init; }

        public GetDirectoryAnalyticsRejection ErrorCode { get; init; }

        public string Reason { get; init; } = null!;
    }
}

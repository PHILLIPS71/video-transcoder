using Giantnodes.Infrastructure.Messages;

namespace Giantnodes.Dashboard.Abstractions.Features.Statistics.Queries.GetDirectoryContainerStatistics
{
    public record GetDirectoryContainerStatisticsRejected : IRejected<GetDirectoryContainerStatisticsRejection>
    {
        public Guid ConversationId { get; init; }

        public DateTime TimeStamp { get; init; }

        public GetDirectoryContainerStatisticsRejection ErrorCode { get; init; }

        public string Reason { get; init; } = null!;
    }
}

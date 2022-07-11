using Giantnodes.Infrastructure.Messages;

namespace Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Queries
{
    public record GetDirectoryContentsRejected : IRejected<GetDirectoryContentsRejection>
    {
        public Guid ConversationId { get; init; }

        public DateTime TimeStamp { get; init; }

        public GetDirectoryContentsRejection ErrorCode { get; init; }

        public string Reason { get; init; } = null!;
    }
}

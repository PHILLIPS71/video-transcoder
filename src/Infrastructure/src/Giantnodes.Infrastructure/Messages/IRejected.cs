namespace Giantnodes.Infrastructure.Messages
{
    public interface IRejected<T> where T : Enum
    {
        Guid ConversationId { get; init; }

        DateTime TimeStamp { get; init; }

        T ErrorCode { get; init; }

        string Reason { get; init; }
    }
}

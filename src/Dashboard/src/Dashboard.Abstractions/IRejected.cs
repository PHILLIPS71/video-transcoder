namespace Giantnodes.Dashboard.Abstractions
{
    public interface IRejected<T> where T : Enum
    {
        Guid ConversationId { get; init; }

        DateTime TimeStamp { get; init; }

        T Code { get; init; }

        string Reason { get; init; }
    }
}

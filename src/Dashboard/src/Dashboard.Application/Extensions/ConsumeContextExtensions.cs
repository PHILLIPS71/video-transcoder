using Giantnodes.Dashboard.Abstractions;
using Giantnodes.Infrastructure;

namespace MassTransit
{
    public static class ConsumeContextExtensions
    {
        public static async Task RejectAsync<T, E>(this ConsumeContext context, E code, string? reason = null)
            where T : class, IRejected<E>
            where E : Enum
        {
            if (context.IsResponseAccepted<T>())
                await context.RespondAsync<T>(new
                {
                    ConversationId = context.ConversationId,
                    TimeStamp = DateTime.UtcNow,
                    Code = code,
                    Reason = reason ?? code.GetStringValue(),
                });

            // Probably log something here so we can track the message if needed
        }
    }
}
namespace Giantnodes.Infrastructure.Messages.Faults
{
    public record GeneralFault
    {
        public string Code { get; init; } = null!;

        public string Reason { get; init; } = null!;
    }
}

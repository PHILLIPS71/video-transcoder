namespace Giantnodes.Infrastructure.Masstransit.Validation
{
    public record InvalidValidationRule
    {
        public string Rule { get; init; } = null!;

        public string Reason { get; init; } = null!;
    }
}

namespace Giantnodes.Infrastructure.Masstransit.Validation
{
    public record InvalidValidationProperty
    {
        public string Property { get; init; } = null!;

        public ICollection<InvalidValidationRule> Issues { get; set; } = new List<InvalidValidationRule>();
    }
}

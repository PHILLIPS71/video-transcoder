namespace Giantnodes.Application.Validation
{
    public record ValidationFault
    {
        public InvalidValidationProperty[] Properties { get; init; } = Array.Empty<InvalidValidationProperty>();
    }
}

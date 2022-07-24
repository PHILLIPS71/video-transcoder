using Giantnodes.Infrastructure.Exceptions;

namespace Giantnodes.Infrastructure.Masstransit.Validation
{
    public class ValidationException : DomainException<ValidationFault>
    {
        public ValidationException(ValidationFault error)
            : base(error, "Validation failed. See errors property for details.")
        {
        }
    }
}

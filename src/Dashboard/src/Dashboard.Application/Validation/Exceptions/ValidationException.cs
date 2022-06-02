using Giantnodes.Infrastructure.Exceptions;

namespace Giantnodes.Application.Validation
{
    public class ValidationException : DomainException<ValidationFault>
    {
        public ValidationException(ValidationFault error)
            : base(error, "Validation failed. See errors property for details.")
        {
        }
    }
}

namespace Giantnodes.Infrastructure.Exceptions
{
    public class DomainException<T> : Exception
    {
        public T Result { get; private set; } = default!;

        public DomainException(T error, string? message = null)
            : base(message ?? "An error occurred")
        {
            Result = error;
        }
    }
}

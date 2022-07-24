namespace Giantnodes.Worker.Domain.Common
{
    public interface IDeletableEntity
    {
        DateTime? DeletedAt { get; set; }

        Guid? DeletedById { get; set; }
    }
}

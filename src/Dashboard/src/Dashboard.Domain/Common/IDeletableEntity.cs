namespace Giantnodes.Dashboard.Domain.Common
{
    public interface IDeletableEntity
    {
        DateTime? DeletedAt { get; set; }

        Guid? DeletedById { get; set; }
    }
}

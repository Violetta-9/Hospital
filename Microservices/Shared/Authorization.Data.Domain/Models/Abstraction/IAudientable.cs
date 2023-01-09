namespace Authorization.Data_Domain.Models.Abstraction;

public interface IAudientable
{
    public DateTimeOffset RowCreatedTimestamp { get; set; }
    public DateTimeOffset LastRowModificationTimestamp { get; set; }
}

public interface IIdentifiable<TIdentifierType>
    where TIdentifierType : IComparable
{
    public TIdentifierType Id { get; set; }
}

public abstract class EntityBase : IAudientable
{
    public DateTimeOffset RowCreatedTimestamp { get; set; }
    public DateTimeOffset LastRowModificationTimestamp { get; set; }
}

public abstract class KeyedEntityBase : EntityBase, IIdentifiable<long>
{
    public long Id { get; set; }
}
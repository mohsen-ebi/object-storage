namespace Simple.Object.Storage.Domain;

public interface IEntity<TKey> : IEntity
{
    TKey Id { get; }
}

public interface IEntity
{
}
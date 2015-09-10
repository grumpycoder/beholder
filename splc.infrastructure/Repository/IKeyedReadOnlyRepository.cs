namespace splc.infrastructure.Repository
{
    public interface IKeyedReadOnlyRepository<TKey, TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class, IKeyed<TKey>
    {
        TEntity FindBy(TKey id);
    }
}

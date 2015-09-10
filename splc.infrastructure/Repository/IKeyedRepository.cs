﻿namespace splc.infrastructure.Repository
{
    public interface IKeyedRepository<TKey, TEntity> : IRepository<TEntity>
        where TEntity : class, IKeyed<TKey>
    {
        TEntity FindBy(TKey id);
    }
}

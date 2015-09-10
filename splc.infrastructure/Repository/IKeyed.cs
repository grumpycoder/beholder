using System;
namespace splc.infrastructure.Repository
{
    public interface IKeyed<TKey>
    {
        TKey Id { get; }
    }
}

using System;

namespace splc.infrastructure.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}

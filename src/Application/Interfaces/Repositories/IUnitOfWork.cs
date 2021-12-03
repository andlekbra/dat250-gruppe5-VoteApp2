using VoteApp.Domain.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace VoteApp.Application.Interfaces.Repositories
{
    public interface IUnitOfWork<TId> : IDisposable
    {
        IRepositoryAsync<T, TId> Repository<T>() where T : AuditableEntity<TId>;

        Task<int> Commit(CancellationToken cancellationToken);

        Task Rollback();
    }
}
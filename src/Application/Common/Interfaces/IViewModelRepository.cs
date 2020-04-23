using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Common.Interfaces
{
    public interface IViewModelRepository<T> where T : ReadModelBase
    {
        Task InsertAsync(T entity, CancellationToken cancellationToken);

        Task<List<T>> GetAsync();

        Task UpdateAsync(T entity);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(Guid id);

        Task RemoveByIdAsync(T entity, CancellationToken cancellationToken);

    }
}
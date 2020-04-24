using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Cloudbash.Application.Common.Interfaces
{
    public interface IViewModelRepository<T> where T : ReadModelBase
    {
        void Insert(T entity);

        IEnumerable<T> Get();

        void Update(T entity);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate);

        T GetById(Guid id);

        void RemoveById(T entity);

    }
}
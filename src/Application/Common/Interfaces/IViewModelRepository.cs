using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cloudbash.Application.Common.Interfaces
{
    public interface IViewModelRepository<T> where T : IReadModel
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(string[] children);
        Task<List<T>> Filter(Expression<Func<T, bool>> filter, string[] children);
        Task<T> GetAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
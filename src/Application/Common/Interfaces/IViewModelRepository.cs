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
        Task<T> GetAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(Guid id);
    }
}
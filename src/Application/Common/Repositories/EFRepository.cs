using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cloudbash.Application.Common.Repositories
{
    public abstract class EfRepository<TEntity, TContext> : IViewModelRepository<TEntity>
        where TEntity : class, IReadModel
        where TContext : IApplicationDbContext
    {
        private readonly TContext _context;
        public EfRepository(TContext context)
        {
            this._context = context;
        }

        public Task<TEntity> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

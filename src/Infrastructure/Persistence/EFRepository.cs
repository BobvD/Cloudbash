using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Persistence
{
    public class EFRepository<T> : IViewModelRepository<T> where T : class, IReadModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EFRepository<T>> _logger;

        public EFRepository(ApplicationDbContext context,
                            ILogger<EFRepository<T>> logger)
        {            
            _context = context;
            _logger = logger;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            Save();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity);
            Save();
        }

        public async Task<List<T>> FilterAsync(Expression<Func<T, bool>> filter, string[] children)
        {
            IQueryable<T> query = _context.Set<T>(); 
            foreach (string entity in children)
            {
                query = query.Include(entity);

            }
            return await query.Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(string[] children)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (string entity in children)
            {
                query = query.Include(entity);
            }
            
            return await query.AsNoTracking().ToListAsync();    
        }

        public Task<T> GetAsync(Guid id)
        {
            return Task.FromResult(_context.Set<T>().Find(id.ToString())); 
        }

        public Task<T> GetAsync(Guid id, string[] children)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (string entity in children)
            {
                query = query.Include(entity);
            }
            
            return Task.FromResult(query.FirstOrDefault(e => e.Id == id.ToString()));

        }

        public Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            Save();
            return Task.FromResult(entity);
        }

        private bool Save()
        {
            try
            {
                return Convert.ToBoolean(_context.SaveChanges());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
}

using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Common.Repositories
{
    public class ConcertEFRepository : IViewModelRepository<Concert>
    {
        private readonly IApplicationDbContext _context;

        public ConcertEFRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Concert> AddAsync(Concert entity)
        {
            _context.Concerts.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Concerts.FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentException();
            }

            _context.Concerts.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Concert>> GetAllAsync()
        {
            return await _context.Concerts.ToListAsync();
        }

        public async Task<Concert> GetAsync(Guid id)
        {
            return await _context.Concerts.FindAsync(id);
        }

        public async Task<Concert> UpdateAsync(Concert entity)
        {
            _context.Concerts.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

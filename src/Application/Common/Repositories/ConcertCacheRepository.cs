

using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cloudbash.Application.Common.Repositories
{
    public class ConcertCacheRepository : IViewModelRepository<Concert>
    {
        private readonly ICache _cache;
        public ConcertCacheRepository(ICache cache)
        {
            _cache = cache;
        }

        public Task<Concert> AddAsync(Concert entity)
        {
            _cache.Save<Concert>(entity);
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Concert>> GetAllAsync()
        {
            List<Concert> concerts = new List<Concert>(_cache.Get<Concert>());
            return Task.FromResult(concerts);
        }

        public Task<Concert> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Concert> UpdateAsync(Concert entity)
        {
            throw new NotImplementedException();
        }
    }
}

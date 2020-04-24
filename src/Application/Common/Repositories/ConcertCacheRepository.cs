

using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Cloudbash.Application.Common.Repositories
{
    public class ConcertCacheRepository : IViewModelRepository<Concert>
    {
        private readonly ICache _cache;
        public ConcertCacheRepository(ICache cache)
        {
            _cache = cache;
        }

        public IEnumerable<Concert> FindAll(Expression<Func<Concert, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Concert> Get()
        {
            return _cache.Get<Concert>();
            
        }

        public Concert GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Concert entity)
        {
            _cache.Save(entity);         
        }

        public void RemoveById(Concert entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Concert entity)
        {
            throw new NotImplementedException();
        }
    }
}

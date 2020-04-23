﻿

using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
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

        public Task<IEnumerable<Concert>> FindAllAsync(Expression<Func<Concert, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Concert> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Concert entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveByIdAsync(Concert entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Concert entity)
        {
            throw new NotImplementedException();
        }
    }
}

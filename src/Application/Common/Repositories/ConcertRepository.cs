using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Common.Repositories
{
    public class ConcertRepository : IViewModelRepository<Concert>
    {
        private readonly IApplicationDbContext _context;

        public ConcertRepository(IApplicationDbContext context)
        {
            _context = context;
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
            _context.Concerts.Add(entity);
            return _context.SaveChangesAsync(cancellationToken);
        }

        public Task UpdateAsync(Concert entity)
        {
            throw new NotImplementedException();
        }
    }
}

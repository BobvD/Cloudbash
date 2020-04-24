using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace Cloudbash.Application.Common.Repositories
{
    public class ConcertEFRepository : IViewModelRepository<Concert>
    {
        private readonly IApplicationDbContext _context;

        public ConcertEFRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Concert> FindAll(Expression<Func<Concert, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Concert> Get()
        {
            return _context.Concerts.ToList();
        }

        public Concert GetById(Guid id)
        {
            return _context.Concerts.Find(id);
        }


        public void Insert(Concert entity)
        {
            _context.Concerts.Add(entity);
            _context.SaveChanges();
        }

        public void RemoveById(Concert concert)
        {
            _context.Concerts.Remove(concert);
            _context.SaveChanges();
        }

        public void Update(Concert entity)
        {
            throw new NotImplementedException();
        }
    }
}

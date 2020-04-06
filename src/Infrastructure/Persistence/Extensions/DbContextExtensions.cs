using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Cloudbash.Infrastructure.Persistence.Extensions
{
    public static class DbContextExtensions
    {
        public static IQueryable<object> Set(this DbContext _context, Type t)
        {
            return (IQueryable<object>)_context.GetType().GetMethod("Set").MakeGenericMethod(t).Invoke(_context, null);
        }
    }
}
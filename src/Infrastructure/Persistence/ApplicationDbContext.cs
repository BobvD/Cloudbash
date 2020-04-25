
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.ViewModels;
using Cloudbash.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Concert> Concerts { get; set; }

      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

        }

        public IQueryable<object> GetSetByObject(Type t)
        {
            return this.Set(t);
        }

        public Task<int> SaveChangesAsync()
        {
            var result = base.SaveChanges();
            return Task.FromResult(result);
        }
    }
}

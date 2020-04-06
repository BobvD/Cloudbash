
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

    }
}

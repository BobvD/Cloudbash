using Cloudbash.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cloudbash.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Concert> Concerts { get; set; }
        IQueryable<object> GetSetByObject(Type t);
        Task<int> SaveChangesAsync();
    }
}

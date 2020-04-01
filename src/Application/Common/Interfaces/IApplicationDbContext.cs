using Cloudbash.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cloudbash.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Concert> Concerts { get; set; }
    }
}

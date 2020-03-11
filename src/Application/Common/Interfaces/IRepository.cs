﻿using Cloudbash.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace Cloudbash.Application.Common.Interfaces
{
    public interface IRepository<TAggregate> where TAggregate : IAggregate
    {
        Task<TAggregate> GetByIdAsync(Guid id);
        Task SaveAsync(TAggregate aggregate);
    }
}

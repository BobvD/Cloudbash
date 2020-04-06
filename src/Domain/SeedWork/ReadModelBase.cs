using System;

namespace Cloudbash.Domain.SeedWork
{
    public abstract class ReadModelBase : IReadModel
    {        
        public Guid Id { get; set; }
    }
}

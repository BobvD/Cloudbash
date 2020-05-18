using System;

namespace Cloudbash.Domain.SeedWork
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }

        public EntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}

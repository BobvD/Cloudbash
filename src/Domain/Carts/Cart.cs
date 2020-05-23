using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Carts
{
    public class Cart : AggregateRootBase
    {
        public Guid CustomerId { get; set; }
    }
}

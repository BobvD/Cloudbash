using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Concerts
{
    public class Ticket : EntityBase
    {
        public Guid CustomerId { get; set; }
        public string Code { get; set; }
    }
}

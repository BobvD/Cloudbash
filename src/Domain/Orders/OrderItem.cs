using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Orders
{
    public class OrderItem : EntityBase
    {
        public Guid TicketId { get; set; }
    }
}

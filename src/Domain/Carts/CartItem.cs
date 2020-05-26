using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.Carts
{
    public class CartItem : EntityBase
    {
        public Guid TicketTypeId { get; set; }
        public int Quantity { get; set; }
    }
}

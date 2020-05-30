using Cloudbash.Domain.SeedWork;
using System;

namespace Cloudbash.Domain.ReadModels
{
    public class CartItem : ReadModelBase
    {
        public string TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }
        public int Quantity { get; set; }
    }
}

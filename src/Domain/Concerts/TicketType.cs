using Cloudbash.Domain.SeedWork;
using System.Collections.Generic;

namespace Cloudbash.Domain.Concerts
{
    public class TicketType : EntityBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        private List<Ticket> Tickets { get; set; }

        public TicketType()
        {
            Tickets = new List<Ticket>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudbash.Domain.Concerts
{
    public class TicketType
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

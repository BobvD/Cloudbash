﻿using Cloudbash.Domain.SeedWork;

namespace Cloudbash.Domain.Concerts
{
    public class TicketType : EntityBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
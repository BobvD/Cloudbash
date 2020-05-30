using Cloudbash.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Cloudbash.Domain.ReadModels
{
    public class Cart : ReadModelBase
    {
        public Guid CustomerId { get; set; }
        public List<CartItem> Items { get; set; }
        public Cart()
        {
            Items = new List<CartItem>();
        }
    }
}



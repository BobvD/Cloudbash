using Cloudbash.Domain.SeedWork;

namespace Cloudbash.Domain.ViewModels
{
    public class TicketType : ReadModelBase
    {  
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Concert Concert { get; set; }
        public TicketType() { }
    }
}

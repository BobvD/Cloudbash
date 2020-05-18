using Cloudbash.Domain.SeedWork;
using Newtonsoft.Json;

namespace Cloudbash.Domain.ViewModels
{
    public class TicketType : ReadModelBase
    {  
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public Concert Concert { get; set; }
        public TicketType() { }
    }
}

using Cloudbash.Domain.ReadModels;
using System.Collections.Generic;

namespace Cloudbash.Application.Concerts.Queries.GetConcerts
{
    public class GetConcertsVm
    {
        public IList<Concert> Concerts { get; set; }
        public int Count { get; set; }
    }
}

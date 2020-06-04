using Cloudbash.Domain.ReadModels;
using System.Collections.Generic;

namespace Cloudbash.Application.Concerts.Queries.FilterConcerts
{
    public class FilterConcertsVm
    {
        public IList<Concert> Concerts { get; set; }
        public int Count { get; set; }
    }
}


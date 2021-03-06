﻿using Cloudbash.Domain.SeedWork;

namespace Cloudbash.Domain.ReadModels
{
    public class Venue : ReadModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string WebUrl { get; set; }
        public string Address { get; set; }

        public Venue() { }
    }
}

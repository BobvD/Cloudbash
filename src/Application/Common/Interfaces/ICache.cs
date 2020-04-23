using System;
using System.Collections.Generic;

namespace Cloudbash.Application.Common.Interfaces
{
    public interface ICache
    {
        void Save<T>(T o) where T : class;
        T Get<T>(Guid id) where T : class;
        IList<T> Get<T>() where T : class;
    }
}

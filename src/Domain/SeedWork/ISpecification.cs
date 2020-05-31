using System;
using System.Linq.Expressions;

namespace Cloudbash.Domain.SeedWork
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> SpecExpression { get; }
        bool IsSatisfiedBy(T obj);
    }
}
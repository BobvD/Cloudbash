using Cloudbash.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cloudbash.Lambda.Functions
{
    public abstract class FunctionBase
    {
        public IServiceProvider _serviceProvider;
        protected IMediator Mediator;

        public FunctionBase() : this(Startup
          .BuildContainer()
          .BuildServiceProvider())
        {
        }

        public FunctionBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            Mediator = _serviceProvider.GetService<IMediator>();          
        }
    }
}

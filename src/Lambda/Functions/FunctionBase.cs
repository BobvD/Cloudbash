﻿using Amazon.Runtime.Internal.Transform;
using Cloudbash.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

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

        protected IDictionary<string, string> GetCorsHeaders()
        {
            return new Dictionary<string, string>()
            {
                new KeyValuePair<string, string>("Access-Control-Allow-Origin", "*"),
                new KeyValuePair<string, string>("Access-Control-Allow-Credentials", "true"),
                new KeyValuePair<string, string>("Access-Control-Allow-Methods", "*")
            };
        }
    }
}
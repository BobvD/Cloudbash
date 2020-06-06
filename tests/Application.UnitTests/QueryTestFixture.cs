using AutoMapper;
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Application.Common.Mappings;
using Cloudbash.Domain.ReadModels;
using Cloudbash.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace Cloudbash.Application.UnitTests
{
    public sealed class QueryTestFixture : IDisposable
    {
        public QueryTestFixture()
        {
            Context = ApplicationDbContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
            
            var cartRepositoryLoggerMock = new Mock<ILogger<EFRepository<Cart>>>();
            CartRepository = new EFRepository<Cart>(Context, cartRepositoryLoggerMock.Object);

            var concertRepositoryLoggerMock = new Mock<ILogger<EFRepository<Concert>>>();
            ConcertRepository = new EFRepository<Concert>(Context, concertRepositoryLoggerMock.Object);
      
        }


        private readonly ApplicationDbContext Context;
        public IViewModelRepository<Cart> CartRepository { get; }
        public IViewModelRepository<Concert> ConcertRepository { get; }

        public IMapper Mapper { get; }

        public void Dispose()
        {
            ApplicationDbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryTests")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}


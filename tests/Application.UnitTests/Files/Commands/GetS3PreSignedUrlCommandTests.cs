
using Cloudbash.Application.Common.Interfaces;
using Cloudbash.Application.Files.Commands.GenerateS3PreSignedUrl;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Cloudbash.Application.UnitTests.Files.Commands
{
    public class GetS3PreSignedUrlCommandTests : CommandTestBase
    {
        private readonly Guid venueId = Guid.NewGuid();

        [Fact]
        public async Task Handle_ShouldReturn_S3URL()
        {
            var command = new GetS3PreSignedUrlCommand
            {
                Filename = "test.png",
                Type = "PNG"
            };

            Mock<IFileService> mock = new Mock<IFileService>();

            mock.Setup(m => m.GetUploadUrl("test.png", "PNG")).Returns("S3_URL");
            
    
            var handler = new GetS3PreSignedUrlCommand.GetS3PreSignedUrlCommandHandler(mock.Object);

            var result = await handler.Handle(command, CancellationToken.None);
            result.ShouldBe("S3_URL");
        }
    }
}

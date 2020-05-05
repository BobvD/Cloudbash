using Cloudbash.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cloudbash.Application.Files.Commands.GenerateS3PreSignedUrl
{
    public class GetS3PreSignedUrlCommand : IRequest<string>
    {
        public string Filename { get; set; }
        public string Type { get; set; }

        public class GetS3PreSignedUrlCommandHandler : IRequestHandler<GetS3PreSignedUrlCommand, string>
        {

            private IFileService _fileService;

            public GetS3PreSignedUrlCommandHandler(IFileService fileService)
            {
                _fileService = fileService;
            }

            public async Task<string> Handle(GetS3PreSignedUrlCommand request, CancellationToken cancellationToken)
            {
                return _fileService.GetUploadUrl(request.Filename, request.Type);
            }
        }
    }
}

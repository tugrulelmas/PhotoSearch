using MediatR;
using PhotoSearch.Api.Entities;
using PhotoSearch.Api.Infrastructure;
using PhotoSearch.Api.Requests;
using PhotoSearch.Api.Services;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoSearch.Api.Handlers
{
    public class GetPhotoSummariesHandler : IRequestHandler<GetPhotoSummariesRequest, IPage<PhotoSummary>>
    {
        private readonly IPhotoService photoService;

        public GetPhotoSummariesHandler(IPhotoService photoService) {
            this.photoService = photoService;
        }

        public async Task<IPage<PhotoSummary>> Handle(GetPhotoSummariesRequest request, CancellationToken cancellationToken) {
            return await photoService.GetPhotoSummariesAsync(request);
        }
    }
}

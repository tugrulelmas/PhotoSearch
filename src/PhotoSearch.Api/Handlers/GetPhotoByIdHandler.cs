using MediatR;
using PhotoSearch.Api.Entities;
using PhotoSearch.Api.Requests;
using PhotoSearch.Api.Services;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoSearch.Api.Handlers
{
    public class GetPhotoByIdHandler : IRequestHandler<GetPhotoByIdRequest, Photo>
    {
        private readonly IPhotoService photoService;

        public GetPhotoByIdHandler(IPhotoService photoService) {
            this.photoService = photoService;
        }

        public async Task<Photo> Handle(GetPhotoByIdRequest request, CancellationToken cancellationToken) {
            return await photoService.GetPhotoAsync(request);
        }
    }
}

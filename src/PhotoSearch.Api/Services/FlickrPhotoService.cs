using PhotoSearch.Api.Entities;
using PhotoSearch.Api.Exceptions;
using PhotoSearch.Api.Infrastructure;
using PhotoSearch.Api.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoSearch.Api.Services
{
    public class FlickrPhotoService : IPhotoService
    {
        private readonly IUrlBuilder urlBuilderService;
        private readonly IHttpClient httpClient;

        public FlickrPhotoService(IUrlBuilder urlBuilderService, IHttpClient httpClient) {
            this.urlBuilderService = urlBuilderService;
            this.httpClient = httpClient;
        }

        public async Task<Photo> GetPhotoAsync(GetPhotoByIdRequest request) {
            var url = urlBuilderService.Method(MethodType.Photo)
                .AddQueryParameter(ParameterName.PhotoId, request.PhotoId.ToString())
                .Build();

            var response = await httpClient.GetAsync<ServiceResponse>(url);
            CheckResponse(response);

            return new Photo {
                Id = Convert.ToInt64(response.Photo.Id),
                Farm = response.Photo.Farm,
                IsFavorite = response.Photo.IsFavorite,
                License = response.Photo.License,
                Rotation = response.Photo.Rotation,
                SafetyLevel = response.Photo.Safety_level,
                Secret = response.Photo.Secret,
                Server = response.Photo.Server,
                UploadedDate = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(response.Photo.dateuploaded)).DateTime,
                Owner = new Owner {
                    Id = response.Photo.Owner.Nsid,
                    Location = response.Photo.Owner.Location,
                    RealName = response.Photo.Owner.Realname,
                    UserName = response.Photo.Owner.Username
                }
            };
        }

        public async Task<IPage<PhotoSummary>> GetPhotoSummariesAsync(GetPhotoSummariesRequest request) {
            var url = urlBuilderService.Method(MethodType.Summary)
                .AddQueryParameter(ParameterName.Keyword, request.Keyword)
                .AddQueryParameter(ParameterName.Lat, request.Lat.HasValue ? request.Lat.ToString() : string.Empty)
                .AddQueryParameter(ParameterName.Lon, request.Lon.HasValue ? request.Lon.ToString() : string.Empty)
                .AddQueryParameter(ParameterName.Page, request.Page.ToString())
                .AddQueryParameter(ParameterName.Limit, request.Limit.ToString())
                .Build();

            var response = await httpClient.GetAsync<SummaryServiceResponse>(url);
            CheckResponse(response);

            var result = new Page<PhotoSummary> {
                Pages = response.Photos.Pages,
                Data = response.Photos.Photo.Select(x =>
                new PhotoSummary {
                    Farm = x.Farm,
                    Id = Convert.ToInt64(x.Id),
                    IsFamily = x.Isfamily,
                    IsFriend = x.Isfriend,
                    IsPublic = x.Ispublic,
                    Owner = x.Owner,
                    Secret = x.Secret,
                    Server = x.Server,
                    Title = x.Title
                }).ToList()
            };
            return result;
        }

        private static void CheckResponse(ServiceBaseResponse response) {
            if (string.Equals(response.Stat, "fail", StringComparison.OrdinalIgnoreCase))
                throw new DenialException(response.Message);
        }

        private class ServiceBaseResponse
        {
            public string Stat { get; set; }

            public string Message { get; set; }
        }

        private class ServiceResponse : ServiceBaseResponse
        {
            public PhotoResponse Photo { get; set; }
        }

        private class PhotoResponse
        {
            public string Id { get; set; }

            public OwnerResponse Owner { get; set; }

            public string Secret { get; set; }

            public string Server { get; set; }

            public int Farm { get; set; }

            public bool IsFavorite { get; set; }

            public int License { get; set; }

            public int Safety_level { get; set; }

            public int Rotation { get; set; }

            public string dateuploaded { get; set; }
        }

        private class OwnerResponse
        {
            public string Nsid { get; set; }

            public string Realname { get; set; }

            public string Username { get; set; }

            public string Location { get; set; }
        }

        private class SummaryServiceResponse : ServiceBaseResponse
        {
            public SummaryPhotosResponse Photos { get; set; }
        }

        private class SummaryPhotosResponse
        {
            public int Pages { get; set; }

            public IEnumerable<SummaryPhotoResponse> Photo { get; set; }
        }

        private class SummaryPhotoResponse
        {
            public string Id { get; set; }

            public string Owner { get; set; }

            public string Secret { get; set; }

            public string Server { get; set; }

            public int Farm { get; set; }

            public string Title { get; set; }

            public bool Ispublic { get; set; }

            public bool Isfriend { get; set; }

            public bool Isfamily { get; set; }
        }
    }
}

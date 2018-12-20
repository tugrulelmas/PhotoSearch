using PhotoSearch.Api.Entities;
using PhotoSearch.Api.Infrastructure;
using PhotoSearch.Api.Requests;
using System.Threading.Tasks;

namespace PhotoSearch.Api.Services
{
    public interface IPhotoService
    {
        /// <summary>
        /// Gets the photo asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<Photo> GetPhotoAsync(GetPhotoByIdRequest request);

        /// <summary>
        /// Gets the photo summaries asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<IPage<PhotoSummary>> GetPhotoSummariesAsync(GetPhotoSummariesRequest request);
    }
}

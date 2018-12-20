using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhotoSearch.Api.Entities;
using PhotoSearch.Api.Infrastructure;
using PhotoSearch.Api.Requests;
using System.Threading.Tasks;

namespace PhotoSearch.Api.Controllers
{
    /// <summary>
    /// PhotosController
    /// </summary>
    [Route("api/[controller]")]
    public class PhotosController : ApiController
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotosController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public PhotosController(IMediator mediator) {
            this.mediator = mediator;
        }

        /// <summary>
        /// Gets the photos.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <param name="lat">The latitude.</param>
        /// <param name="lon">The longitude.</param>
        /// <param name="page">The page.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IPage<PhotoSummary>>> Get(string keyword, double? lat = null, double? lon = null, int page = 1, int limit = 20) {
            IPage<PhotoSummary> result = await mediator.Send(new GetPhotoSummariesRequest(keyword, lat, lon, page, limit));
            return Ok(result);
        }

        /// <summary>
        /// Gets the photo.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> Get(long id) {
            var result = await mediator.Send(new GetPhotoByIdRequest(id));
            return Ok(result);
        }
    }
}

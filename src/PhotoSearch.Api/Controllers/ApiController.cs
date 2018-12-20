using Microsoft.AspNetCore.Mvc;
using PhotoSearch.Api.ActionDecorators;

namespace PhotoSearch.Api.Controllers
{
    /// <summary>
    /// ApiController
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    [ServiceFilter(typeof(ActionDecoratorFilter))]
    public abstract class ApiController : ControllerBase
    {
    }
}

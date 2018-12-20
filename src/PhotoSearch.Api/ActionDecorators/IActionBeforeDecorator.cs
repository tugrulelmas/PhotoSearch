using Microsoft.AspNetCore.Mvc.Filters;

namespace PhotoSearch.Api.ActionDecorators
{
    public interface IActionBeforeDecorator
    {
        /// <summary>
        /// Decorates the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        void Decorate(ActionExecutingContext context);
    }
}

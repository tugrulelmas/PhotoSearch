using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace PhotoSearch.Api.ActionDecorators
{
    public interface IActionExceptionDecorator
    {
        /// <summary>
        /// Decorates the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        void Decorate(ActionExecutingContext context, Exception exception);
    }
}

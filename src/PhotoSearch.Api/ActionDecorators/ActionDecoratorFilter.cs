using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSearch.Api.ActionDecorators
{
    public class ActionDecoratorFilter : IAsyncActionFilter
    {
        private readonly IEnumerable<IActionBeforeDecorator> beforeDecorators;
        private readonly IEnumerable<IActionExceptionDecorator> exceptionDecorators;

        public ActionDecoratorFilter(IEnumerable<IActionBeforeDecorator> beforeDecorators, IEnumerable<IActionExceptionDecorator> exceptionDecorators) {
            this.beforeDecorators = beforeDecorators;
            this.exceptionDecorators = exceptionDecorators;
        }

        /// <summary>
        /// Called when action is being executed asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="next">The next.</param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            try {
                foreach (var decoratorItem in beforeDecorators) {
                    decoratorItem.Decorate(context);
                }

                await next();
            } catch (Exception ex) {
                foreach (var decoratorItem in exceptionDecorators) {
                    decoratorItem.Decorate(context, ex);
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PhotoSearch.Api.Exceptions.Adapters;
using System;

namespace PhotoSearch.Api.ActionDecorators
{
    public class ExceptionDecorator : IActionExceptionDecorator
    {
        private readonly IExceptionAdapterFactory exceptionAdapterFactory;

        public ExceptionDecorator(IExceptionAdapterFactory exceptionAdapterFactory) {
            this.exceptionAdapterFactory = exceptionAdapterFactory;
        }

        public void Decorate(ActionExecutingContext context, Exception exception) {
            IExceptionAdapter exceptionAdapter = exceptionAdapterFactory.GetAdapter(exception);

            context.HttpContext.Response.StatusCode = (int)exceptionAdapter.HttpStatusCode;

            if (exceptionAdapter.ExtraHeaders != null) {
                foreach (var headerItem in exceptionAdapter.ExtraHeaders) {
                    if (context.HttpContext.Response.Headers.ContainsKey(headerItem.Key))
                        continue;

                    context.HttpContext.Response.Headers.Add(headerItem.Key, headerItem.Value);
                }
            }
            context.Result = new JsonResult(exceptionAdapter.Content);
        }
    }
}

using PhotoSearch.Api.Entities;

namespace PhotoSearch.Api.Services
{
    public interface IUrlBuilder
    {
        /// <summary>
        /// Sets the specified method type.
        /// </summary>
        /// <param name="methodType">Type of the method.</param>
        /// <returns></returns>
        IUrlBuilder Method(MethodType methodType);

        /// <summary>
        /// Adds the query parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        IUrlBuilder AddQueryParameter(ParameterName parameterName, string value);

        /// <summary>
        /// Builds the url.
        /// </summary>
        /// <returns></returns>
        string Build();
    }
}

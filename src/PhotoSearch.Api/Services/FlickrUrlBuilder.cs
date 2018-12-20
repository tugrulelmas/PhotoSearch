using Microsoft.Extensions.Configuration;
using PhotoSearch.Api.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoSearch.Api.Services
{
    public class FlickrUrlBuilder : IUrlBuilder
    {
        protected MethodType methodType;
        protected readonly string jsonFormatter;
        protected readonly string restServiceUrl;
        protected readonly string apiKey;
        protected readonly IDictionary<ParameterName, string> queryParameters;
        protected readonly IDictionary<MethodType, string> methodMaps;
        protected readonly IDictionary<ParameterName, string> parameterMaps;

        public FlickrUrlBuilder(IConfiguration configuration) {
            jsonFormatter = "&format=json&nojsoncallback=1";
            restServiceUrl = "https://api.flickr.com/services/rest/?";
            apiKey = "&api_key=" + configuration.GetValue<string>("flickr-api-key");
            queryParameters = new Dictionary<ParameterName, string>();

            methodMaps = new Dictionary<MethodType, string> {
                { MethodType.Photo, "getInfo" },
                { MethodType.Summary, "search" }
            };

            parameterMaps = new Dictionary<ParameterName, string> {
                { ParameterName.PhotoId, "photo_id" },
                { ParameterName.Keyword, "text" },
                { ParameterName.Lat, "lat" },
                { ParameterName.Lon, "lon" },
                { ParameterName.Page, "page" },
                { ParameterName.Limit, "per_page" }
            };
        }

        protected FlickrUrlBuilder(IConfiguration configuration, IDictionary<ParameterName, string> parameterMaps)
            : this(configuration) {
            this.parameterMaps = parameterMaps;
        }

        public IUrlBuilder AddQueryParameter(ParameterName parameterName, string value) {
            if (string.IsNullOrWhiteSpace(value))
                return this;

            if (queryParameters.ContainsKey(parameterName))
                throw new Exception($"{parameterName} is already exists.");

            if (!parameterMaps.ContainsKey(parameterName))
                throw new Exception($"{parameterName} is invalid.");

            queryParameters.Add(parameterName, value);

            return this;
        }

        public IUrlBuilder Method(MethodType methodType) {
            this.methodType = methodType;

            return this;
        }

        public string Build() {
            var builder = new StringBuilder();
            builder.Append(restServiceUrl);
            builder.Append($"method=flickr.photos.{methodMaps[methodType]}");
            foreach (var queryParameterItem in queryParameters) {
                builder.Append($"&{parameterMaps[queryParameterItem.Key]}={queryParameterItem.Value}");
            }
            builder.Append(apiKey);
            builder.Append(jsonFormatter);

            return builder.ToString();
        }
    }
}

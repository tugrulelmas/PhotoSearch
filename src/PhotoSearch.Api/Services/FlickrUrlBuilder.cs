using PhotoSearch.Api.Entities;
using PhotoSearch.Api.Services;
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
        protected readonly IDictionary<ParameterName, string> queryParameters;
        protected readonly IDictionary<MethodType, string> methodMaps;
        protected readonly IDictionary<MethodType, string> keyMaps;
        protected readonly IDictionary<ParameterName, string> parameterMaps;

        public FlickrUrlBuilder() {
            jsonFormatter = "&format=json&nojsoncallback=1";
            restServiceUrl = "https://api.flickr.com/services/rest/?";
            queryParameters = new Dictionary<ParameterName, string>();

            methodMaps = new Dictionary<MethodType, string> {
                { MethodType.Photo, "getInfo" },
                { MethodType.Summary, "search" }
            };

            keyMaps = new Dictionary<MethodType, string> {
                { MethodType.Photo, "&api_key=64e2540dc05a8ff75de57f8b102b6364" },
                { MethodType.Summary,"&api_key=9098edcf1042f4c9445e4f63e75a840e" }
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

        protected FlickrUrlBuilder(IDictionary<ParameterName, string> parameterMaps)
            : this() {
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
            builder.Append(keyMaps[methodType]);
            builder.Append(jsonFormatter);

            return builder.ToString();
        }
    }
}

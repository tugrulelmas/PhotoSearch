using PhotoSearch.Api.Entities;
using PhotoSearch.Api.Services;
using System.Collections.Generic;

namespace PhotoSearch.Api.Tests.Stubs
{
    internal class FlickrUrlBuilderStub : FlickrUrlBuilder
    {
        public FlickrUrlBuilderStub() {

        }

        public FlickrUrlBuilderStub(IDictionary<ParameterName, string> parameterMaps)
            : base(parameterMaps) {
        }

        public IDictionary<ParameterName, string> QueryParameters => queryParameters;

        public IDictionary<MethodType, string> KeyMaps => keyMaps;

        public string JsonFormatter => jsonFormatter;

        public string RestServiceUrl => restServiceUrl;

        public MethodType MethodType => methodType;
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using PhotoSearch.Api.Entities;
using PhotoSearch.Api.Services;
using System.Collections.Generic;

namespace PhotoSearch.Api.Tests.Stubs
{
    internal class FlickrUrlBuilderStub : FlickrUrlBuilder
    {
        public FlickrUrlBuilderStub()
            : base(ConfigurationTest.Default) {
        }

        public FlickrUrlBuilderStub(IDictionary<ParameterName, string> parameterMaps)
            : base(ConfigurationTest.Default, parameterMaps) {
        }

        public IDictionary<ParameterName, string> QueryParameters => queryParameters;

        public string ApiKey => apiKey;

        public string JsonFormatter => jsonFormatter;

        public string RestServiceUrl => restServiceUrl;

        public MethodType MethodType => methodType;

        private class ConfigurationTest : IConfiguration
        {
            private readonly IDictionary<string, string> maps;

            public ConfigurationTest(IDictionary<string, string> maps) {
                this.maps = maps;
            }

            public string this[string key] { get { return maps[key]; } set { maps[key] = value; } }

            public IEnumerable<IConfigurationSection> GetChildren() => new Mock<IEnumerable<IConfigurationSection>>().Object;

            public IChangeToken GetReloadToken() => new Mock<IChangeToken>().Object;

            public IConfigurationSection GetSection(string key) {
                var section = new Mock<IConfigurationSection>();
                section.Setup(x => x.Key).Returns(key);
                section.Setup(x => x.Value).Returns(maps[key]);
                return section.Object;
            }

            public static IConfiguration Default => new ConfigurationTest(new Dictionary<string, string> { { "flickr-api-key", "api-key" } });
        }
    }
}

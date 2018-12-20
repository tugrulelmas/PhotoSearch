using PhotoSearch.Api.Entities;
using PhotoSearch.Api.Tests.Stubs;
using System;
using System.Collections.Generic;
using Xunit;

namespace PhotoSearch.Api.Tests
{
    public class FlickrUrlBuilderShould
    {
        [Fact]
        public void NotAddQueryParameterIfTheValueIsEmpty() {
            var builder = new FlickrUrlBuilderStub();
            var count = builder.QueryParameters.Count;

            builder.AddQueryParameter(ParameterName.PhotoId, string.Empty);

            Assert.Equal(count, builder.QueryParameters.Count);
        }

        [Fact]
        public void ThrowExceptionIfTheParameterNameIsAlreadyExists() {
            var builder = new FlickrUrlBuilderStub();

            builder.AddQueryParameter(ParameterName.PhotoId, "2");
            var exception = Assert.Throws<Exception>(() => builder.AddQueryParameter(ParameterName.PhotoId, "2"));

            Assert.Equal($"{ParameterName.PhotoId} is already exists.", exception.Message);
        }

        [Fact]
        public void ThrowExceptionIfTheParameterIsInvalid() {
            var builder = new FlickrUrlBuilderStub(new Dictionary<ParameterName, string> {
                { ParameterName.Keyword, "text" }
            });
            
            var exception = Assert.Throws<Exception>(() => builder.AddQueryParameter(ParameterName.PhotoId, "2"));

            Assert.Equal($"{ParameterName.PhotoId} is invalid.", exception.Message);
        }

        [Fact]
        public void AddQueryParameter() {
            var builder = new FlickrUrlBuilderStub();

            builder.AddQueryParameter(ParameterName.PhotoId, "2");

            Assert.Equal(1, builder.QueryParameters.Count);
            Assert.Equal("2", builder.QueryParameters[ParameterName.PhotoId]);
        }

        [Fact]
        public void SetMethodName() {
            var builder = new FlickrUrlBuilderStub();

            builder.Method(MethodType.Photo);
            
            Assert.Equal(MethodType.Photo, builder.MethodType);
        }

        [Fact]
        public void BuildUrl() {
            var builder = new FlickrUrlBuilderStub();

            var url = builder.Method(MethodType.Photo)
                            .AddQueryParameter(ParameterName.PhotoId, "12")
                            .AddQueryParameter(ParameterName.Keyword, "test")
                            .Build();

            Assert.Equal($"{builder.RestServiceUrl}method=flickr.photos.getInfo&photo_id=12&text=test{builder.ApiKey}{builder.JsonFormatter}", url);
        }

        [Fact]
        public void ContainAllParameters() {
            var builder = new FlickrUrlBuilderStub();

            var allParameters = Enum.GetValues(typeof(ParameterName));
            foreach (ParameterName item in allParameters ) {
                builder.AddQueryParameter(item, "test");
            }

            Assert.Equal(allParameters.Length, builder.QueryParameters.Count);
        }
    }
}

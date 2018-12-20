using FluentValidation;
using PhotoSearch.Api.Requests;
using System.Linq;
using Xunit;

namespace PhotoSearch.Api.Tests
{
    public class GetPhotoSummariesRequestValidatorShould
    {
        [Fact]
        public void ReturnAnErrorIfTheAllParametersEmpty() {
            IValidator<GetPhotoSummariesRequest> validator = new GetPhotoSummariesRequestValidator();

            var result = validator.Validate(new GetPhotoSummariesRequest(string.Empty, null, null, 1, 1));

            Assert.False(result.IsValid);
            Assert.Equal("YouShouldPassAtLeastOneParameter", result.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-12)]
        public void ReturnAnErrorIfThePageLessThanOrEqualToZero(int page) {
            IValidator<GetPhotoSummariesRequest> validator = new GetPhotoSummariesRequestValidator();

            var result = validator.Validate(new GetPhotoSummariesRequest("test", null, null, page, 1));

            Assert.False(result.IsValid);
            Assert.Equal("PageShouldBeGreaterThanZero", result.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-12)]
        public void ReturnAnErrorIfTheLimitLessThanOrEqualToZero(int limit) {
            IValidator<GetPhotoSummariesRequest> validator = new GetPhotoSummariesRequestValidator();

            var result = validator.Validate(new GetPhotoSummariesRequest("test", null, null, 1, limit));

            Assert.False(result.IsValid);
            Assert.Equal("LimitShouldBeGreaterThanZero", result.Errors.First().ErrorMessage);
        }

        [Fact]
        public void ReturnAnErrorIfTheLimitGreaterThan400() {
            IValidator<GetPhotoSummariesRequest> validator = new GetPhotoSummariesRequestValidator();

            var result = validator.Validate(new GetPhotoSummariesRequest("test", null, null, 1, 4001));

            Assert.False(result.IsValid);
            Assert.Equal("LimitShouldBeLessThan4000", result.Errors.First().ErrorMessage);
        }

        [Fact]
        public void PassAllCasesIfTheParametersAreValid() {
            IValidator<GetPhotoSummariesRequest> validator = new GetPhotoSummariesRequestValidator();

            var result = validator.Validate(new GetPhotoSummariesRequest("test", null, null, 1, 200));

            Assert.True(result.IsValid);
        }
    }
}

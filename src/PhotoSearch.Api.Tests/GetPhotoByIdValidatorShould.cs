using FluentValidation;
using PhotoSearch.Api.Requests;
using System.Linq;
using Xunit;

namespace PhotoSearch.Api.Tests
{
    public class GetPhotoByIdValidatorShould
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-12)]
        public void ReturnAnErrorIfThePhotoIdLessThanOrEqualToZero(int id) {
            IValidator<GetPhotoByIdRequest> validator = new GetPhotoByIdRequestValidator();

            var result = validator.Validate(new GetPhotoByIdRequest(id));

            Assert.False(result.IsValid);
            Assert.Equal("PhotoIdShouldBeGreaterThanZero", result.Errors.First().ErrorMessage);
        }

        [Fact]
        public void PassAllCasesIfThePhotoIdGreaterThanZero() {
            IValidator<GetPhotoByIdRequest> validator = new GetPhotoByIdRequestValidator();

            var result = validator.Validate(new GetPhotoByIdRequest(12));

            Assert.True(result.IsValid);
        }
    }
}

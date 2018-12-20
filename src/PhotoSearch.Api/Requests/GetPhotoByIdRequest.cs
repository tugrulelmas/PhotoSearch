using System;
using FluentValidation;
using MediatR;
using PhotoSearch.Api.Entities;
using PhotoSearch.Api.Infrastructure;

namespace PhotoSearch.Api.Requests
{
    public class GetPhotoByIdRequest : IRequest<Photo>, ICacheable
    {
        public GetPhotoByIdRequest(long photoId) {
            PhotoId = photoId;
        }

        public long PhotoId { get; }

        public string CacheKey => $"PhotoById_{PhotoId}";

        public TimeSpan Duration => TimeSpan.FromMinutes(20);

    }

    public class GetPhotoByIdRequestValidator : AbstractValidator<GetPhotoByIdRequest>
    {
        public GetPhotoByIdRequestValidator() {
            RuleFor(r => r.PhotoId).GreaterThan(0).WithMessage("PhotoIdShouldBeGreaterThanZero");
        }
    }
}

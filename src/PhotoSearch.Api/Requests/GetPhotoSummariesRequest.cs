using System;
using FluentValidation;
using MediatR;
using PhotoSearch.Api.Entities;
using PhotoSearch.Api.Infrastructure;

namespace PhotoSearch.Api.Requests
{
    public class GetPhotoSummariesRequest : IRequest<IPage<PhotoSummary>>, ICacheable
    {
        public GetPhotoSummariesRequest(string keyword, double? lat, double? lon, int page, int limit) {
            Keyword = keyword;
            Lat = lat;
            Lon = lon;
            Page = page;
            Limit = limit;
        }

        public string Keyword { get; }

        public double? Lat { get; }

        public double? Lon { get; }

        public int Page { get; }

        public int Limit { get; }

        public string CacheKey => $"PhotoSummaries_{Keyword}#{Lat}#{Lon}";

        public TimeSpan Duration => TimeSpan.FromMinutes(10);
    }

    public class GetPhotoSummariesRequestValidator : AbstractValidator<GetPhotoSummariesRequest>
    {
        public GetPhotoSummariesRequestValidator() {
            RuleFor(r => r.Keyword).NotEmpty().When(x => !x.Lat.HasValue && !x.Lon.HasValue).WithMessage("YouShouldPassAtLeastOneParameter");
            RuleFor(r => r.Page).GreaterThan(0).WithMessage("PageShouldBeGreaterThanZero");
            RuleFor(r => r.Limit).GreaterThan(0).WithMessage("LimitShouldBeGreaterThanZero");
            RuleFor(r => r.Limit).LessThan(4000).WithMessage("LimitShouldBeLessThan4000");
        }
    }
}

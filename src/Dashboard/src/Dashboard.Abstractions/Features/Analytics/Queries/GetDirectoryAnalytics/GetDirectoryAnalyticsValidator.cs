using FluentValidation;

namespace Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries
{
    public class GetDirectoryAnalyticsValidator : AbstractValidator<GetDirectoryAnalytics>
    {
        public GetDirectoryAnalyticsValidator()
        {
            RuleFor(p => p.Directory)
                .NotEmpty();
        }
    }
}

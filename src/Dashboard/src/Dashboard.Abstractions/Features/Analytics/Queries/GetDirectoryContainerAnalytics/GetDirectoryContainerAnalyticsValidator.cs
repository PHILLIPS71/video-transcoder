using FluentValidation;

namespace Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries
{
    public class GetDirectoryContainerAnalyticsValidator : AbstractValidator<GetDirectoryContainerAnalytics>
    {
        public GetDirectoryContainerAnalyticsValidator()
        {
            RuleFor(p => p.Directory)
                .NotEmpty();
        }
    }
}

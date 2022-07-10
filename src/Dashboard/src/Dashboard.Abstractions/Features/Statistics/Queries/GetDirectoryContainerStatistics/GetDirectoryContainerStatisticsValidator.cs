using FluentValidation;

namespace Giantnodes.Dashboard.Abstractions.Features.Statistics.Queries.GetDirectoryContainerStatistics
{
    public class GetDirectoryContainerStatisticsValidator : AbstractValidator<GetDirectoryContainerStatistics>
    {
        public GetDirectoryContainerStatisticsValidator()
        {
            RuleFor(p => p.Directory)
                .NotEmpty();
        }
    }
}

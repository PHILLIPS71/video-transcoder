using System.ComponentModel;

namespace Giantnodes.Dashboard.Abstractions.Features.Statistics.Queries.GetDirectoryContainerStatistics
{
    public enum GetDirectoryContainerStatisticsRejection
    {
        [Description("The directory cannot be found.")]
        DIRECTORY_NOT_FOUND,
    }
}

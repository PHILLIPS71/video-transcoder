using System.ComponentModel;

namespace Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries
{
    public enum GetDirectoryContainerAnalyticsRejection
    {
        [Description("The directory cannot be found.")]
        DIRECTORY_NOT_FOUND,
    }
}

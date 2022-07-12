using System.ComponentModel;

namespace Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries
{
    public enum GetDirectoryAnalyticsRejection
    {
        [Description("The directory cannot be found.")]
        DIRECTORY_NOT_FOUND,
    }
}

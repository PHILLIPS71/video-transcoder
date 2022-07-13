using Giantnodes.Dashboard.Abstractions.Common;

namespace Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries
{
    public record GetDirectoryAnalyticsResult
    {
        public FileSystemFile? LatestModifiedFile { get; init; }

        public int TotalFiles { get; init; }

        public long WatchTimeMinutes { get; init; }
    }
}

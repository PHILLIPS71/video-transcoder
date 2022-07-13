namespace Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries
{
    public record GetDirectoryContainerAnalyticsResult
    {
        /// <summary>
        /// A array of video file containers and the statistics around it
        /// </summary>
        public DirectoryContainerAnalytics[] Containers { get; init; } = Array.Empty<DirectoryContainerAnalytics>();
    }

    public record DirectoryContainerAnalytics
    {
        /// <summary>
        /// The file extension found in a directory
        /// </summary>
        public string Extension { get; init; } = string.Empty;

        /// <summary>
        /// The total amount of files in the directory that uses the extension
        /// </summary>
        public int TotalFiles { get; init; }

        /// <summary>
        /// The percentage of the directory that uses the extension
        /// </summary>
        public decimal Percent { get; init; }
    }
}

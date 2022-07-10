namespace Giantnodes.Dashboard.Abstractions.Features.Statistics.Queries.GetDirectoryContainerStatistics
{
    public record GetDirectoryContainerStatisticsResult
    {
        /// <summary>
        /// A array of video file containers and the statistics around it
        /// </summary>
        public DirectoryContainerStatistic[] Containers { get; init; } = Array.Empty<DirectoryContainerStatistic>();
    }

    public record DirectoryContainerStatistic
    {
        /// <summary>
        /// The file extension found in a directory
        /// </summary>
        public string Extension { get; init; } = string.Empty;

        /// <summary>
        /// The percentage of the file system that uses the extension
        /// </summary>
        public decimal Percent { get; init; }
    }
}

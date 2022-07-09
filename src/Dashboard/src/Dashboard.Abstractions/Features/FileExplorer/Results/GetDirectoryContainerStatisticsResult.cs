namespace Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Results
{
    public record GetDirectoryContainerStatisticsResult
    {
        /// <summary>
        /// A array of video file containers and the statistics around it
        /// </summary>
        public DirectoryContainerStatistic[] Containers { get; init; } = Array.Empty<DirectoryContainerStatistic>();
    }
}

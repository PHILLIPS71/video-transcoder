namespace Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Queries
{
    public record GetDirectoryContainerStatisticsQuery
    {
        /// <summary>
        /// A directories full path.
        /// </summary>
        public string Directory { get; init; } = null!;
    }
}

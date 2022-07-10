namespace Giantnodes.Dashboard.Abstractions.Features.Statistics.Queries.GetDirectoryContainerStatistics
{
    public record GetDirectoryContainerStatistics
    {
        /// <summary>
        /// A directories full path.
        /// </summary>
        public string Directory { get; init; } = null!;
    }
}

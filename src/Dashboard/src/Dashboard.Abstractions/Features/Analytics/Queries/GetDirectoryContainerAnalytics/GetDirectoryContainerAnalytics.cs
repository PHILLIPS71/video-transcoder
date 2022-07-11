namespace Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries
{
    public record GetDirectoryContainerAnalytics
    {
        /// <summary>
        /// A directories full path.
        /// </summary>
        public string Directory { get; init; } = null!;
    }
}

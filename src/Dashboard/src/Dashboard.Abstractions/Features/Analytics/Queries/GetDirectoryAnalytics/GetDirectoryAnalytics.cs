namespace Giantnodes.Dashboard.Abstractions.Features.Analytics.Queries
{
    public record GetDirectoryAnalytics
    {
        /// <summary>
        /// A directories full path.
        /// </summary>
        public string Directory { get; init; } = null!;
    }
}

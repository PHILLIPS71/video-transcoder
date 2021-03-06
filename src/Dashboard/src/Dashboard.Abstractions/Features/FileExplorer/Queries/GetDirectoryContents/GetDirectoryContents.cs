namespace Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Queries
{
    public record GetDirectoryContents
    {
        /// <summary>
        /// A directories full path.
        /// </summary>
        public string Directory { get; init; } = null!;
    }
}

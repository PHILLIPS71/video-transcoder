namespace Giantnodes.Dashboard.Abstractions.Features.FileExplorer
{
    public record GetDirectoryContentsQuery
    {
        /// <summary>
        /// A directories full path.
        /// </summary>
        public string Directory { get; init; } = null!;
    }
}

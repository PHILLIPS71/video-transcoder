namespace Giantnodes.Dashboard.Abstractions.Features.FileExplorer
{
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

using Giantnodes.Dashboard.Abstractions.Common;

namespace Giantnodes.Dashboard.Abstractions.Features.FileExplorer
{
    public record GetDirectoryContentsResult
    {
        /// <summary>
        /// The array of directories found inside a node in a file system hierarchy.
        /// </summary>
        public FileSystemDirectory[] Directories { get; init; } = new FileSystemDirectory[0];

        /// <summary>
        /// The array of files found inside a node in a file system hierarchy.
        /// </summary>
        public FileSystemFile[] Files { get; init; } = new FileSystemFile[0];
    }
}

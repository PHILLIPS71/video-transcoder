using Giantnodes.Dashboard.Abstractions.Common;

namespace Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Queries
{
    public record GetDirectoryContentsResult
    {
        /// <summary>
        /// The array of directories found inside a node in a file system hierarchy.
        /// </summary>
        public FileSystemDirectory[] Directories { get; init; } = Array.Empty<FileSystemDirectory>();

        /// <summary>
        /// The array of files found inside a node in a file system hierarchy.
        /// </summary>
        public FileSystemFile[] Files { get; init; } = Array.Empty<FileSystemFile>();
    }
}

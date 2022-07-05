namespace Giantnodes.Dashboard.Abstractions.Common
{
    /// <summary>
    /// Represents a file in a file system hierarchy.
    /// </summary>
    public record FileSystemFile : IFileSystemNode
    {
        /// <inheritdoc />
        public string Path { get; set; } = null!;

        /// <inheritdoc />
        public string Name { get; set; } = null!;

        /// <summary>
        /// Defines if the current file is read only.
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        ///  The full path of the directory the file is in.
        /// </summary>
        public string? DirectoryName { get; set; }

        /// <inheritdoc />
        public DateTime CreatedAtUtc { get; set; }

        /// <inheritdoc />
        public DateTime ModifiedAtUtc { get; set; }
    }
}

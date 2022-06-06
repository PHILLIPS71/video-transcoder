namespace Giantnodes.Dashboard.Abstractions.Common
{
    /// <summary>
    /// Represents node in a file system hierarchy.
    /// </summary>
    public interface IFileSystemNode
    {
        /// <summary>
        ///  The full path of the file or directory.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// The name of the file or directory.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The size in bytes of the current file or directory.
        /// </summary>
        long Length { get; set; }

        /// <summary>
        ///  The creation time in coordinated universal time (UTC)
        /// </summary>
        DateTime CreatedAtUtc { get; set; }

        /// <summary>
        ///  The last modified time in coordinated universal time (UTC)
        /// </summary>
        DateTime ModifiedAtUtc { get; set; }
    }
}

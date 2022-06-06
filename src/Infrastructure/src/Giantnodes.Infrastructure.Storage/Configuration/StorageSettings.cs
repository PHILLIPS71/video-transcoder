namespace Giantnodes.Infrastructure.Storage.Configuration
{
    /// <summary>
    /// Represents the storage settings options
    /// </summary>
    public class StorageSettings
    {
        /// <summary>
        /// Gets or sets the target directory where files can be stored or read from.
        /// </summary>
        public string Directory { get; set; } = null!;
    }
}

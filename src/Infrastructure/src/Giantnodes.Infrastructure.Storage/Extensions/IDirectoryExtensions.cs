namespace System.IO.Abstractions
{
    public static class IDirectoryExtensions
    {
        /// <summary>
        /// Asynchronously determines whether the given path refers to an 
        /// existing directory on disk. If nothing is found after 8 seconds
        /// the task will timeout.
        /// </summary>
        /// <param name="path">The path to test.</param>
        /// <param name="cancellation">A cancellation token.</param>
        /// <returns> 
        /// true if path refers to an existing directory; false if the 
        /// directory does not exist or an error occurs when trying to 
        /// determine if the specified directory exists.
        // </returns>
        public static async Task<bool> ExistsAsync(this IDirectory directory, string path, CancellationToken cancellation = default)
        {
            try
            {
                var task = Task.Run(() => directory.Exists(path));
                await task.WaitAsync(TimeSpan.FromSeconds(8), cancellation);
                return task.Result;
            }
            catch
            {
                return false;
            }
        }
    }
}

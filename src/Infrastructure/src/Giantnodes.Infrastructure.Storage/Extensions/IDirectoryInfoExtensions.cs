namespace System.IO.Abstractions
{
    public static class IDirectoryInfoExtensions
    {
        /// <summary>
        /// Gets the size of a directory looking in all sub-directories.
        /// </summary>
        /// <returns>The directories size in bytes.</returns>
        public static long GetSize(this IDirectoryInfo directory)
        {
            long size = 0;
            IFileInfo[] files = directory.GetFiles();
            foreach (IFileInfo file in files)
            {
                size += file.Length;
            }

            IDirectoryInfo[] directories = directory.GetDirectories();
            foreach (IDirectoryInfo dir in directories)
            {
                size += GetSize(dir);
            }

            return size;
        }
    }
}

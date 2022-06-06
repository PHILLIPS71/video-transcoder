namespace System.IO
{
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// Gets the size of a directory looking in all sub-directories.
        /// </summary>
        /// <param name="directory">A directory to caclualte the size of.</param>
        /// <returns>The directories size in bytes.</returns>
        public static long GetSize(this DirectoryInfo directory)
        {
            long size = 0;
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                size += file.Length;
            }

            DirectoryInfo[] directories = directory.GetDirectories();
            foreach (DirectoryInfo dir in directories)
            {
                size += GetSize(dir);
            }

            return size;
        }
    }
}

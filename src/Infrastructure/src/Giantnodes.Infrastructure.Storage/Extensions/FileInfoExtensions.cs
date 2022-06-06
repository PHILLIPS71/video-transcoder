namespace System.IO
{
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Determines if the specified file is a media file by comparing
        /// it's extension to a predefined list of accepted media types.
        /// </summary>
        /// <param name="file">A file to check the media type of.</param>
        /// <returns></returns>
        public static bool IsMediaFile(this FileInfo file)
        {
            var extension = file.Extension;
            if (string.IsNullOrEmpty(extension))
                return false;

            return _extensions.Contains(extension);
        }

        private static HashSet<string> _extensions = new HashSet<string>()
        {
            { ".webm" },

            //SDTV
            { ".m4v" },
            { ".3gp" },
            { ".nsv" },
            { ".ty" },
            { ".strm" },
            { ".rm" },
            { ".rmvb" },
            { ".m3u" },
            { ".ifo" },
            { ".mov" },
            { ".qt" },
            { ".divx" },
            { ".xvid" },
            { ".bivx" },
            { ".nrg" },
            { ".pva" },
            { ".wmv" },
            { ".asf" },
            { ".asx" },
            { ".ogm" },
            { ".ogv" },
            { ".m2v" },
            { ".avi" },
            { ".bin" },
            { ".dat" },
            { ".dvr-ms" },
            { ".mpg" },
            { ".mpeg" },
            { ".mp4" },
            { ".avc" },
            { ".vp3" },
            { ".svq3" },
            { ".nuv" },
            { ".viv" },
            { ".dv" },
            { ".fli" },
            { ".flv" },
            { ".wpl" },
            
            //DVD
            { ".img" },
            { ".iso" },
            { ".vob" },

            //HD
            { ".mkv" },
            { ".ts" },
            { ".wtv" },

            //Bluray
            { ".m2ts" }
        };
    }
}

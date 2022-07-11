﻿using System.ComponentModel;

namespace Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Queries
{
    public enum GetDirectoryContentsRejection
    {
        [Description("The directory cannot be found.")]
        DIRECTORY_NOT_FOUND,
    }
}

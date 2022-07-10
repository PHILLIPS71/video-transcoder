using System.ComponentModel;

namespace Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Queries.GetDirectoryContents
{
    public enum GetDirectoryContentsRejection
    {
        [Description("The directory cannot be found.")]
        DIRECTORY_NOT_FOUND,
    }
}

using Giantnodes.Dashboard.Abstractions.Common;
using Giantnodes.Dashboard.Abstractions.Features.FileExplorer;
using MassTransit;

namespace Giantnodes.Dashboard.Api.FileSystem
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class FileSystemResolver
    {
        [UseSorting]
        public async Task<IEnumerable<IFileSystemNode>> DirectoryContents(
            [Service] IRequestClient<GetDirectoryContentsQuery> client,
            GetDirectoryContentsQuery input,
            CancellationToken cancellation
        )
        {
            Response response = await client.GetResponse<GetDirectoryContentsResult>(input, cancellation);
            return response switch
            {
                (_, GetDirectoryContentsResult result) => Concat(result),
                _ => throw new InvalidOperationException()
            };
        }

        /// <summary>
        /// Concats both directories and files into a single list using <see cref="AddRange()"/> 
        /// as Concat chaining is much less performant.
        /// </summary>
        /// <param name="result">The result of the <see cref="GetDirectoryContentsQuery"/>.</param>
        /// <returns>A IEnumerable containing both file system directoties and files.</returns>
        private static IEnumerable<IFileSystemNode> Concat(GetDirectoryContentsResult result)
        {
            var storage = new List<IFileSystemNode>();
            storage.AddRange(result.Directories);
            storage.AddRange(result.Files);
            return storage;
        }
    }
}

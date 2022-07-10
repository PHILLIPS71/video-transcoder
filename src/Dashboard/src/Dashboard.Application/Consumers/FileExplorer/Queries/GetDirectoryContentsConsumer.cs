using Giantnodes.Dashboard.Abstractions.Common;
using Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Queries.GetDirectoryContents;
using MassTransit;
using System.IO.Abstractions;

namespace Giantnodes.Dashboard.Application.Consumers.FileExplorer.Queries
{
    public class GetDirectoryContentsConsumer : IConsumer<GetDirectoryContents>
    {
        private readonly IFileSystem _system;

        public GetDirectoryContentsConsumer(IFileSystem system)
        {
            this._system = system;
        }

        public async Task Consume(ConsumeContext<GetDirectoryContents> context)
        {
            if (!_system.Directory.Exists(context.Message.Directory))
            {
                await context.RejectAsync<GetDirectoryContentsRejected, GetDirectoryContentsRejection>(GetDirectoryContentsRejection.DIRECTORY_NOT_FOUND);
                return;
            }

            var directory = _system.DirectoryInfo.FromDirectoryName(context.Message.Directory);
            var files = directory.GetFiles()
                .Where(file => file.IsMediaFile())
                .Select(file => new FileSystemFile
                {
                    Path = file.FullName,
                    Name = file.Name,
                    DirectoryName = file.DirectoryName,
                    IsReadOnly = file.IsReadOnly,
                    CreatedAtUtc = file.CreationTimeUtc,
                    ModifiedAtUtc = file.LastWriteTimeUtc
                }).ToArray();

            var directories = directory.GetDirectories()
                .Select(directory => new FileSystemDirectory
                {
                    Path = directory.FullName,
                    Name = directory.Name,
                    CreatedAtUtc = directory.CreationTimeUtc,
                    ModifiedAtUtc = directory.LastWriteTimeUtc
                }).ToArray();

            await context.RespondAsync<GetDirectoryContentsResult>(new { Directories = directories, Files = files });
        }
    }
}

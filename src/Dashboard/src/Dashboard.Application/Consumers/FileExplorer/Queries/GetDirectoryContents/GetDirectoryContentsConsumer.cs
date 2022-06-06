using Giantnodes.Dashboard.Abstractions.Common;
using Giantnodes.Dashboard.Abstractions.Features.FileExplorer;
using MassTransit;

namespace Giantnodes.Dashboard.Application.Consumers.FileExplorer
{
    public class GetDirectoryContentsConsumer : IConsumer<GetDirectoryContentsQuery>
    {
        public async Task Consume(ConsumeContext<GetDirectoryContentsQuery> context)
        {
            if (!Directory.Exists(context.Message.Directory))
                return;

            var directory = new DirectoryInfo(context.Message.Directory);
            var files = directory.GetFiles()
                .Where(file => file.IsMediaFile())
                .Select(file => new FileSystemFile
                {
                    Path = file.FullName,
                    Name = file.Name,
                    Length = file.Length,
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
                    Length = directory.GetSize(),
                    CreatedAtUtc = directory.CreationTimeUtc,
                    ModifiedAtUtc = directory.LastWriteTimeUtc
                }).ToArray();

            await context.RespondAsync<GetDirectoryContentsResult>(new { Directories = directories, Files = files });
        }
    }
}

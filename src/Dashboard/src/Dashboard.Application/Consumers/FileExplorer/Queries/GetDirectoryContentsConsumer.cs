using Giantnodes.Dashboard.Abstractions.Common;
using Giantnodes.Dashboard.Abstractions.Features.FileExplorer.Queries;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using System.IO.Abstractions;

namespace Giantnodes.Dashboard.Application.Consumers.FileExplorer.Queries
{
    public class GetDirectoryContentsConsumer : IConsumer<GetDirectoryContents>
    {
        protected string? CacheKey { get; private set; }
        protected readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(15);

        private readonly IDistributedCache _cache;
        private readonly IFileSystem _system;

        public GetDirectoryContentsConsumer(IDistributedCache cache, IFileSystem system)
        {
            this._cache = cache;
            this._system = system;
        }

        public async Task Consume(ConsumeContext<GetDirectoryContents> context)
        {
            CacheKey = $"file-system:contents:{context.Message.Directory}";

            if (!await _system.Directory.ExistsAsync(context.Message.Directory))
            {
                await context.RejectAsync<GetDirectoryContentsRejected, GetDirectoryContentsRejection>(GetDirectoryContentsRejection.DIRECTORY_NOT_FOUND);
                return;
            }

            var cached = await _cache.GetAsync<GetDirectoryContentsResult>(CacheKey);
            if (cached != null)
            {
                await context.RespondAsync<GetDirectoryContentsResult>(cached);
                return;
            }

            var directory = _system.DirectoryInfo.FromDirectoryName(context.Message.Directory);
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

            var result = new GetDirectoryContentsResult { Directories = directories, Files = files };
            await _cache.SetAsync(CacheKey, result, CacheDuration, context.CancellationToken);
            await context.RespondAsync<GetDirectoryContentsResult>(result);
        }
    }
}

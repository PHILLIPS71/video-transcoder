using Giantnodes.Dashboard.Abstractions.Common;

namespace Giantnodes.Dashboard.Api.Types
{
    public class FileSystemFileType : ObjectType<FileSystemFile>
    {
        protected override void Configure(IObjectTypeDescriptor<FileSystemFile> descriptor)
        {
            descriptor.Field("length").Type<LongType>().Resolve(context =>
            {
                var file = new FileInfo(context.Parent<FileSystemFile>().Path);
                return file.Length;
            });
        }
    }
}

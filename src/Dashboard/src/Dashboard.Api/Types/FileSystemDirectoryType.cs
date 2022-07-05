using Giantnodes.Dashboard.Abstractions.Common;

namespace Giantnodes.Dashboard.Api.Types
{
    public class FileSystemDirectoryType : ObjectType<FileSystemDirectory>
    {
        protected override void Configure(IObjectTypeDescriptor<FileSystemDirectory> descriptor)
        {
            descriptor.Field("length").Type<LongType>().Resolve(context =>
            {
                var directory = new DirectoryInfo(context.Parent<FileSystemDirectory>().Path);
                return directory.GetSize();
            });
        }
    }
}

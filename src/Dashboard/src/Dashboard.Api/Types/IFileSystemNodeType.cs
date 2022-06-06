namespace Giantnodes.Dashboard.Api.Types
{
    public class IFileSystemNodeType : UnionType
    {
        protected override void Configure(IUnionTypeDescriptor descriptor)
        {
            descriptor.Type<FileSystemFileType>();
            descriptor.Type<FileSystemDirectoryType>();
        }
    }
}

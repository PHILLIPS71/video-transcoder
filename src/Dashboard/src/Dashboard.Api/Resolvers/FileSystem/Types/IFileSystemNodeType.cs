namespace Giantnodes.Dashboard.Api.Resolvers
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

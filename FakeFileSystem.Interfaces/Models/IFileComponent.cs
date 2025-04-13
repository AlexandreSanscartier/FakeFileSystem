namespace FakeFileSystem.Interfaces.Models
{
    public interface IFileComponent : IFileSystemComponent
    {
        public string Content { get; set; }
    }
}

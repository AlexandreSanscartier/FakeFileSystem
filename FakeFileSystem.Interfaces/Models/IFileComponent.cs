namespace FakeFileSystem.Interfaces.Models
{
    public interface IFileComponent : IFileSystemComponent
    {
        public string Content { get; }

        public void SetContent(string content);
    }
}

using FakeFileSystem.Interfaces;

namespace FakeFileSystem.CLI.Interfaces.Utilities
{
    public interface IFFSystemService
    {
        IFFSystem FakeFileSystem { get; }
    }
}

using FakeFileSystem.CLI.Interfaces.Utilities;
using FakeFileSystem.Interfaces;

namespace FakeFileSystem.CLI.ConsoleLibrary.Utilities
{
    public sealed class FFSystemService : IFFSystemService
    {
        public IFFSystem FakeFileSystem { get; private set; }

        public FFSystemService(IFFSystem fakeFileSystem)
        {
            FakeFileSystem = fakeFileSystem;
        }
    }
}

using FakeFileSystem.CLI.Interfaces.Utilities;
using FakeFileSystem.Interfaces;

namespace FakeFileSystem.CLI.ConsoleLibrary.Utilities
{
    public class FFSystemService : IFFSystemService
    {
        private IFFSystem _fakeFileSystem;

        public IFFSystem FakeFileSystem => _fakeFileSystem;

        public FFSystemService()
        {
            _fakeFileSystem = new InMemoryFFileSystem();
        }
    }
}

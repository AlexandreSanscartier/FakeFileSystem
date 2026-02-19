using FakeFileSystem.CLI.Interfaces.Commands.Directories;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Directories
{
    public sealed class DirectoryExistsCommand : IDirectoryCommandWithResult<bool>
    {
        private readonly string _path;

        private bool _result;

        private IDirectoryService _directoryService;

        public bool Result => _result;

        public DirectoryExistsCommand(IDirectoryService directoryService, string path)
        {
            _directoryService = directoryService;
            _path = path;
        }

        public void Execute()
        {
            _result = _directoryService.DirectoryExists(_path);
        }
    }
}

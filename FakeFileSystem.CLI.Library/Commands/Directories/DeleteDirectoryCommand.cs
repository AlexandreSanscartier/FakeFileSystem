using FakeFileSystem.CLI.Interfaces.Commands.Directories;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Directories
{
    public sealed class DeleteDirectoryCommand : IDirectoryCommand
    {
        private readonly string _path;

        private readonly bool _recursive;

        private IDirectoryService _directoryService;

        public DeleteDirectoryCommand(IDirectoryService directoryService, string path)
        {
            _directoryService = directoryService;
            _path = path;
            _recursive = false;
        }

        public DeleteDirectoryCommand(IDirectoryService directoryService, string path, bool recursive)
        {
            _directoryService = directoryService;
            _path = path;
            _recursive = recursive;
        }

        public void Execute()
        {
            if (_directoryService.DirectoryExists(_path))
                _directoryService.DeleteDirectory(_path, _recursive);
        }
    }
}

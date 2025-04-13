using FakeFileSystem.CLI.Interfaces.Commands.Directories;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Directories
{
    public class CreateDirectoryCommand : IDirectoryCommand
    {
        private string _directoryName;

        private IDirectoryService _directoryService;

        public CreateDirectoryCommand(IDirectoryService directoryService, string directoryName)
        {
            _directoryService = directoryService;
            _directoryName = directoryName;
        }

        public void Execute()
        {
            _directoryService.CreateDirectory(_directoryName);
        }
    }
}

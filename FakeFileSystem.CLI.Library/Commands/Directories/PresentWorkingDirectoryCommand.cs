using FakeFileSystem.CLI.Interfaces.Commands.Directories;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Directories
{
    public sealed class PresentWorkingDirectoryCommand : IDirectoryCommand
    {
        private IDirectoryService _directoryService;

        public PresentWorkingDirectoryCommand(IDirectoryService directoryService)
        {
            _directoryService = directoryService;
        }
        public void Execute()
        {
            var currentDirectory = _directoryService.GetCurrentDirectory();
            Console.WriteLine(currentDirectory);
        }
    }
}

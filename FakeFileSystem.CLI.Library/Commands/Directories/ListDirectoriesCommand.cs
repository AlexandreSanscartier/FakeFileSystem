using FakeFileSystem.CLI.Interfaces.Commands.Directories;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Directories
{
    public class ListDirectoriesCommand : IDirectoryCommand
    {
        private string _path;

        private IDirectoryService _directoryService;

        public ListDirectoriesCommand(IDirectoryService directoryService, string path)
        {
            _directoryService = directoryService;
            _path = path;
        }

        public void Execute()
        {
            var directories = _directoryService.GetDirectories(_path);
            Console.WriteLine(string.Join(Environment.NewLine, directories));
        }
    }
}

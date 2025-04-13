using FakeFileSystem.CLI.Interfaces.Commands.Directories;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Directories
{
    public class ListFilesCommand : IDirectoryCommand
    {
        private string _path;

        private IDirectoryService _directoryService;

        public ListFilesCommand(IDirectoryService directoryService, string path)
        {
            _directoryService = directoryService;
            _path = path;
        }

        public void Execute()
        {
            var files = _directoryService.GetFiles(_path);
            Console.WriteLine(string.Join(Environment.NewLine, files));
        }
    }
}

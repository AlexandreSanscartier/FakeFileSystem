using FakeFileSystem.CLI.Interfaces.Commands.Directories;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Directories
{
    public sealed class ListAllCommand : IDirectoryCommand
    {
        private string _path;

        private IDirectoryService _directoryService;

        public ListAllCommand(IDirectoryService directoryService, string path)
        {
            _directoryService = directoryService;
            _path = path;
        }

        public void Execute()
        {
            var directories = _directoryService.GetDirectories(_path);
            var files = _directoryService.GetFiles(_path);
            var filesAndDirectories = new List<string>();
            filesAndDirectories.AddRange(directories);
            filesAndDirectories.AddRange(files);
            filesAndDirectories.Sort();
            Console.WriteLine(string.Join(Environment.NewLine, filesAndDirectories));
        }
    }
}

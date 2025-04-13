using FakeFileSystem.CLI.Interfaces.Commands.Files;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Files
{
    public sealed class WriteAllTextCommand : IFileCommand
    {
        private IFileService _fileService;

        private readonly string _path;

        private readonly string _contents;

        public WriteAllTextCommand(IFileService fileService, string path, string contents)
        {
            _fileService = fileService;
            _path = path;
            _contents = contents;
        }

        public void Execute()
        {
            _fileService.WriteAllText(_path, _contents);
        }
    }
}

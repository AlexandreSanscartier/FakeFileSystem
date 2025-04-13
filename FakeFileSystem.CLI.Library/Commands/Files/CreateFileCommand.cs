using FakeFileSystem.CLI.Interfaces.Commands.Files;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Files
{
    public class CreateFileCommand : IFileCommandWithResult<Stream>
    {
        IFileService _fileService;

        private readonly string _path;

        private Stream _result = null!;

        public Stream Result => _result;

        public CreateFileCommand(IFileService fileService, string path)
        {
            _fileService = fileService;
            _path = path;
        }

        public void Execute()
        {
            _result = _fileService.CreateFile(_path);
        }
    }
}

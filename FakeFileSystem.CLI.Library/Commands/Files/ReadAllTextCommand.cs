using FakeFileSystem.CLI.Interfaces.Commands.Files;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Files
{
    public class ReadAllTextCommand : IFileCommandWithResult<string>
    {
        IFileService _fileService;

        private readonly string _path;

        private string _result;

        public string Result => _result;

        public ReadAllTextCommand(IFileService fileService, string path)
        {
            _fileService = fileService;
            _path = path;
            _result = string.Empty;
        }

        public void Execute()
        {
            _result = _fileService.ReadAllText(_path);
        }
    }
}

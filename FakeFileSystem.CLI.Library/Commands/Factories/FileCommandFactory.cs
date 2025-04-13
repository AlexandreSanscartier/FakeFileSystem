using FakeFileSystem.CLI.ConsoleLibrary.Commands.Files;
using FakeFileSystem.CLI.Interfaces.Commands;
using FakeFileSystem.CLI.Interfaces.Commands.Factories;
using FakeFileSystem.CLI.Interfaces.Commands.Models;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Factories
{
    public class FileCommandFactory : IFileCommandFactory
    {
        IFileCommandParameters _fileCommandParameters;

        public FileCommandFactory(IFileCommandParameters fileCommandParameters)
        {
            _fileCommandParameters = fileCommandParameters;
        }

        public ICommand Create(Type fileCommandType)
        {
            var command = fileCommandType switch
            {
                Type _ when fileCommandType == typeof(CreateFileCommand) => BuildCreateFileCommand(),
                Type _ when fileCommandType == typeof(DeleteFileCommand) => BuildDeleteFileCommand(),
                Type _ when fileCommandType == typeof(FileExistsCommand) => BuildFileExistsCommand(),
                Type _ when fileCommandType == typeof(ReadAllTextCommand) => BuildReadAllTextCommand(),
                Type _ when fileCommandType == typeof(WriteAllTextCommand) => BuildWriteAllTextCommand(),
                _ => new NullCommand()
            };

            ResetParameters();
            return command;
        }

        public void SetContent(string content)
        {
            _fileCommandParameters.SetContents(content);
        }

        public void SetFileCommandParameter(IFileCommandParameters fileCommandParaemeters)
        {
            _fileCommandParameters = fileCommandParaemeters;
        }

        public void SetPath(string path)
        {
            _fileCommandParameters.SetPath(path);
        }

        private ICommand BuildCreateFileCommand()
        {
            return new CreateFileCommand(_fileCommandParameters.FileService, _fileCommandParameters.Path);
        }

        private ICommand BuildDeleteFileCommand()
        {
            return new DeleteFileCommand(_fileCommandParameters.FileService, _fileCommandParameters.Path);
        }

        private ICommand BuildFileExistsCommand()
        {
            return new FileExistsCommand(_fileCommandParameters.FileService, _fileCommandParameters.Path);
        }

        private ICommand BuildReadAllTextCommand()
        {
            return new ReadAllTextCommand(_fileCommandParameters.FileService, _fileCommandParameters.Path);
        }

        private ICommand BuildWriteAllTextCommand()
        {
            return new WriteAllTextCommand(_fileCommandParameters.FileService, _fileCommandParameters.Path, _fileCommandParameters.Contents);
        }

        private void ResetParameters()
        {
            SetContent(string.Empty);
            SetPath(string.Empty);
        }
    }
}

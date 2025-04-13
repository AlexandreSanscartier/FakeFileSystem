using FakeFileSystem.CLI.ConsoleLibrary.Commands.Directories;
using FakeFileSystem.CLI.Interfaces.Commands;
using FakeFileSystem.CLI.Interfaces.Commands.Factories;
using FakeFileSystem.CLI.Interfaces.Commands.Models;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Factories
{
    public sealed class DirectoryCommandFactory : IDirectoryCommandFactory
    {
        private IDirectoryCommandParameters _directoryCommandParameters;

        public DirectoryCommandFactory(IDirectoryCommandParameters directoryCommandParameters)
        {
            _directoryCommandParameters = directoryCommandParameters;
        }

        public void SetDirectoryCommandParameter(IDirectoryCommandParameters directoryCommandParameters)
        {
            _directoryCommandParameters = directoryCommandParameters;
        }

        public ICommand Create(Type directoryCommandType)
        {
            var command = directoryCommandType switch
            {
                Type _ when directoryCommandType == typeof(CreateDirectoryCommand) => BuildCreateDirectoryCommand(),
                Type _ when directoryCommandType == typeof(DeleteDirectoryCommand) => BuildDeleteDirectoryCommand(),
                Type _ when directoryCommandType == typeof(DirectoryExistsCommand) => BuildDirectoryExistsCommand(),
                Type _ when directoryCommandType == typeof(ListDirectoriesCommand) => BuildListDirectoriesCommand(),
                Type _ when directoryCommandType == typeof(ListFilesCommand) => BuildListFilesCommand(),
                Type _ when directoryCommandType == typeof(ListAllCommand) => BuildListAllCommand(),
                Type _ when directoryCommandType == typeof(PresentWorkingDirectoryCommand) => BuildPresentWorkingDirectoryCommand(),
                Type _ when directoryCommandType == typeof(SetCurrentDirectoryCommand) => BuildSetCurrentDirectoryCommand(),
                _ => new NullCommand()
            };

            ResetParameters();
            return command;
        }

        public void SetPath(string path)
        {
            _directoryCommandParameters.Path = path;
        }

        private ICommand BuildSetCurrentDirectoryCommand()
        {
            return new SetCurrentDirectoryCommand(_directoryCommandParameters.DirectoryService,
                _directoryCommandParameters.PathService, _directoryCommandParameters.Path);
        }

        private ICommand BuildCreateDirectoryCommand()
        {
            return new CreateDirectoryCommand(_directoryCommandParameters.DirectoryService, _directoryCommandParameters.Path);
        }

        private ICommand BuildDeleteDirectoryCommand()
        {
            return new DeleteDirectoryCommand(_directoryCommandParameters.DirectoryService, _directoryCommandParameters.Path);
        }

        private ICommand BuildDirectoryExistsCommand()
        {
            return new DirectoryExistsCommand(_directoryCommandParameters.DirectoryService, _directoryCommandParameters.Path);
        }

        private ICommand BuildListDirectoriesCommand()
        {
            return new ListDirectoriesCommand(_directoryCommandParameters.DirectoryService, _directoryCommandParameters.Path);
        }

        private ICommand BuildPresentWorkingDirectoryCommand()
        {
            return new PresentWorkingDirectoryCommand(_directoryCommandParameters.DirectoryService);
        }

        private ICommand BuildListAllCommand()
        {
            return new ListAllCommand(_directoryCommandParameters.DirectoryService, _directoryCommandParameters.Path);
        }

        private ICommand BuildListFilesCommand()
        {
            return new ListFilesCommand(_directoryCommandParameters.DirectoryService, _directoryCommandParameters.Path);
        }

        private void ResetParameters()
        {
            SetPath(string.Empty);
        }
    }
}

namespace FakeFileSystem.CLI.Interfaces.Commands.Directories
{
    public interface IDirectoryCommandWithResult<T> : IDirectoryCommand, ICommandWithResult<T>
    {
    }
}

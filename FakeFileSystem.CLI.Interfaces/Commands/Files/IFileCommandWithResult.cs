namespace FakeFileSystem.CLI.Interfaces.Commands.Files
{
    public interface IFileCommandWithResult<T> : IFileCommand, ICommandWithResult<T>
    {
    }
}

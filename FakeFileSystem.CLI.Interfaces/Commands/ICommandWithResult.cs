namespace FakeFileSystem.CLI.Interfaces.Commands
{
    public interface ICommandWithResult<T> : ICommand
    {
        T Result { get; }
    }
}

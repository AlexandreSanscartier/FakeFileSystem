using System.Windows.Input;

namespace FakeFileSystem.CLI.Interfaces.Commands.Services
{
    public interface ICommandRunner
    {
        void Execute(ICommand command);

        T Execute<T>(ICommand command);
    }
}

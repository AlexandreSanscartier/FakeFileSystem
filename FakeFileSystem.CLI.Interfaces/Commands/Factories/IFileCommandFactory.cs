using FakeFileSystem.CLI.Interfaces.Commands.Models;

namespace FakeFileSystem.CLI.Interfaces.Commands.Factories
{
    public interface IFileCommandFactory
    {
        void SetFileCommandParameter(IFileCommandParameters fileCommandParaemeters);

        ICommand Create(Type fileCommandType);

        void SetPath(string path);

        void SetContent(string content);
    }
}

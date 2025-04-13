using FakeFileSystem.CLI.Interfaces.Commands;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands
{
    public class NullCommand : ICommand
    {
        public void Execute()
        {
            // Does nothing
        }
    }
}

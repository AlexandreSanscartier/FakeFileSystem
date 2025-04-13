using FakeFileSystem.CLI.Interfaces.Commands;
using ValueOf;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands
{
    public class CommandAlias : ValueOf<string, CommandAlias>, ICommandAlias
    {
        public string Key => Value;

        protected override void Validate()
        {
            // Empty for now
        }
    }
}

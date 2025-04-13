using FakeFileSystem.CLI.ConsoleLibrary.Utilities;
using FakeFileSystem.CLI.Interfaces.Components;
using FakeFileSystem.CLI.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Components
{
    public class ConsoleHeaderComponent : IHeaderComponent
    {
        private IOutputWriter _outputWriter;

        public ConsoleHeaderComponent(IOutputWriter outputWriter)
        {
            _outputWriter = outputWriter;
        }

        public void PrintHeader(string header)
        {
            _outputWriter.WriteLine(ConsoleUtilities.RepeatCharacter('=', 50));
            _outputWriter.Write(ConsoleUtilities.RepeatCharacter('\t', 2));
            _outputWriter.WriteLine($"{header}");
            _outputWriter.WriteLine(ConsoleUtilities.RepeatCharacter('=', 50));
        }
    }
}

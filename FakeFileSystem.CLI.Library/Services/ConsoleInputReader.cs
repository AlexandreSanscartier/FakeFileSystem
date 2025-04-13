using FakeFileSystem.CLI.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Services
{
    public class ConsoleInputReader : IInputReader
    {
        public string ReadInput()
        {
            var input = Console.ReadLine();
            return input ?? string.Empty;
        }
    }
}

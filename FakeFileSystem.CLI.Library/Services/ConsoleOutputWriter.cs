using FakeFileSystem.CLI.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Services
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        public void Write(string output)
        {
            Console.Write(output);
        }

        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }
    }
}

namespace FakeFileSystem.CLI.Interfaces.Services
{
    public interface IOutputWriter
    {
        void Write(string output);

        void WriteLine(string output);
    }
}

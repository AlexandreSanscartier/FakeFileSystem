using System.Text;

namespace FakeFileSystem.Interfaces.Factories
{
    public interface IMemoryStreamFactory
    {
        MemoryStream Create(string value);

        MemoryStream Create(string value, Encoding encoding);
    }
}

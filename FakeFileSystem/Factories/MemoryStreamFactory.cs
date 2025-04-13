using FakeFileSystem.Interfaces.Factories;
using System.Text;

namespace FakeFileSystem.Factories
{
    public class MemoryStreamFactory : IMemoryStreamFactory
    {
        public MemoryStream Create(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? string.Empty));
        }

        public MemoryStream Create(string value, Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(value ?? string.Empty));
        }
    }
}

using FakeFileSystem.Interfaces.Services;
using System.Text;

namespace FakeFileSystem.Services.Physical
{
    internal class PhysicalFileService : IFileService
    {
        public Stream CreateFile(string path) => File.Create(path);

        public Stream CreateFile(string path, int bufferSize) => File.Create(path, bufferSize);

        public Stream CreateFile(string path, int bufferSize, FileOptions options) => File.Create(path, bufferSize, options);

        public void DeleteFile(string path) => File.Delete(path);

        public bool FileExists(string path) => File.Exists(path);

        public string ReadAllText(string path) => File.ReadAllText(path);

        public string[] ReadAllLines(string path) => File.ReadAllLines(path);

        public byte[] ReadAllBytes(string path) => File.ReadAllBytes(path);

        public void WriteAllText(string path, string contents) => File.WriteAllText(path, contents);

        public void WriteAllText(string path, string contents, Encoding encoding) => File.WriteAllText(path, contents, encoding);

        public void WriteAllText(string path, ReadOnlySpan<char> contents) => File.WriteAllText(path, contents);

        public void WriteAllText(string path, ReadOnlySpan<char> contents, Encoding encoding) => File.WriteAllText(path, contents, encoding);

        public void AppendAllText(string path, string contents) => File.AppendAllText(path, contents);

        public void AppendAllText(string path, string contents, Encoding encoding) => File.AppendAllText(path, contents, encoding);

        public void AppendAllText(string path, ReadOnlySpan<char> contents) => File.AppendAllText(path, contents);

        public void AppendAllText(string path, ReadOnlySpan<char> contents, Encoding encoding) => File.AppendAllText(path, contents, encoding);

        public void AppendAllLines(string path, IEnumerable<string> contents) => File.AppendAllLines(path, contents);

        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding) => File.AppendAllLines(path, contents, encoding);

        public void AppendAllLines(string path, byte[] contents) => File.AppendAllBytes(path, contents);

        public void AppendAllLines(string path, ReadOnlySpan<byte> contents, Encoding encoding) => File.AppendAllBytes(path, contents);
    }
}

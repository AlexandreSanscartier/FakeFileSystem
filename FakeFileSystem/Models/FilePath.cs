using FakeFileSystem.Interfaces.Models;
using ValueOf;

namespace FakeFileSystem.Models
{
    public class FilePath : ValueOf<(string path, string filename), FilePath>, IFilePath
    {
        public string Path => Value.path;

        public string FileName => Value.filename;
    }
}

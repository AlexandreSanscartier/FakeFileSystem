using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.Services.Physical
{
    internal class PhysicalPathService : IPathService
    {
        public char AltDirectorySeperator => Path.AltDirectorySeparatorChar;
        public char DirectorySeperator => Path.DirectorySeparatorChar;

        public string ChangeExtension(string path, string extension) => Path.ChangeExtension(path, extension);

        public string CombinePath(string[] pathParts) => Path.Combine(pathParts);

        public string CombinePath(string path1, string path2) => Path.Combine(path1, path2);

        public string CombinePath(string path1, string path2, string path3) => Path.Combine(path1, path2, path3);
        public string CombinePath(string path1, string path2, string path3, string path4) 
            => Path.Combine(path1, path2, path3, path4);

        public bool EndsInDirectorySeperator(string path) => Path.EndsInDirectorySeparator(path);

        public string GetExtension(string path) => Path.GetExtension(path);

        public string GetFileName(string path) => Path.GetFileName(path);

        public string GetFileNameWithoutExtension(string path) => Path.GetFileNameWithoutExtension(path);

        public ReadOnlySpan<char> GetFileNameWithoutExtension(ReadOnlySpan<char> path) => Path.GetFileNameWithoutExtension(path);

        public char[] GetInvalidFileNameChars() => Path.GetInvalidFileNameChars();

        public char[] GetInvalidPathChars() => Path.GetInvalidPathChars();

        public bool HasExtension(string fileName) => Path.HasExtension(fileName);
        public bool HasExtension(ReadOnlySpan<char> fileName) => Path.HasExtension(fileName);

        public bool IsPathFullyQualified(string path) => Path.IsPathFullyQualified(path);

        public bool IsPathFullyQualified(ReadOnlySpan<char> path) => Path.IsPathFullyQualified(path);

        public bool IsPathRooted(string path) => Path.IsPathRooted(path);

        public bool IsPathRooted(ReadOnlySpan<char> path) => Path.IsPathRooted(path);

        public string[] SplitPath(string path) => path.Split(DirectorySeperator);
    }
}

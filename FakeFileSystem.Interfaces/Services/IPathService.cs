namespace FakeFileSystem.Interfaces.Services
{
    public interface IPathService
    {
        char AltDirectorySeperator { get; }

        char DirectorySeperator { get; }

        string ChangeExtension(string path, string extension);

        string CombinePath(string[] pathParts);

        string CombinePath(string path1, string path2);

        string CombinePath(string path1, string path2, string path3);

        bool EndsInDirectorySeperator(string path);

        string GetExtension(string path);

        string GetFileName(string path);

        string GetFileNameWithoutExtension(string path);

        string[] GetInvalidFileNameChars();

        string[] GetInvalidPathChars();

        bool HasExtension(string fileName);

        bool IsPathRooted(string path);

        bool IsPathFullyQualified(string path);

        string[] SplitPath(string path);
    }
}

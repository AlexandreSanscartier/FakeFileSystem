using FakeFileSystem.Interfaces.Models.FileSystems;
using FakeFileSystem.Interfaces.Services;
using System.Text;
using System.Text.RegularExpressions;

namespace FakeFileSystem.Services
{
    public class InMemoryPathService : IPathService
    {
        private readonly IFileSystemDirectorySeperator _fileSystemDirectorySeperator;

        private readonly string _PathRootedRegex;

        private readonly string _PathFullyQualifiedRegex;

        public char AltDirectorySeperator => _fileSystemDirectorySeperator.AltDirectorySeperator;

        public char DirectorySeperator => _fileSystemDirectorySeperator.DirectorySeperator;

        public InMemoryPathService(IFileSystemDirectorySeperator fileSystemDirectorySeperator)
        {
            _fileSystemDirectorySeperator = fileSystemDirectorySeperator;
            _PathRootedRegex = GeneratePathRootedRegex();
            _PathFullyQualifiedRegex = GeneratePathFullyQualifiedRegex();
        }

        public string ChangeExtension(string path, string extension)
        {
            var fileNameParts = path.Split('.');

            if (fileNameParts.Length == 1)
                return $"{fileNameParts[0]}{extension}";

            return $"{string.Join('.', fileNameParts.SkipLast(1))}{extension}";
        }

        public string CombinePath(string[] pathParts)
        {
            return string.Join(DirectorySeperator, pathParts);
        }

        public string CombinePath(string path1, string path2)
        {
            return $"{ path1}{DirectorySeperator}{path2}";
        }

        public string CombinePath(string path1, string path2, string path3)
        {
            return $"{path1}{DirectorySeperator}{path2}{DirectorySeperator}{path3}";
        }

        public bool EndsInDirectorySeperator(string path)
        {
            return path.Last().Equals(DirectorySeperator) || path.Last().Equals(AltDirectorySeperator);
        }

        public string GetExtension(string path)
        {
            var fileName = GetFileName(path);
            var fileNameParts = fileName.Split('.');

            if (fileNameParts.Length <= 1)
                return string.Empty;

            var fileExtension = $".{fileNameParts.Last()}";
            return fileExtension;
        }

        public string GetFileName(string path)
        {
            var pathParts = SplitPath(path);
            var fileName = pathParts.Last();
            return fileName;
        }

        public string GetFileNameWithoutExtension(string path)
        {
            var fileName = GetFileName(path);
            var fileNameParts = fileName.Split('.');

            if(fileNameParts.Length == 1)
                return fileNameParts[0];

            var fileNameWithoutExtension = string.Join('.', fileNameParts.SkipLast(1));
            return fileNameWithoutExtension;
        }
        public char[] GetInvalidFileNameChars() => [];

        public char[] GetInvalidPathChars() => [];

        public bool HasExtension(string fileName) => Regex.IsMatch(fileName, @"(?:\.[a-zA-Z0-9\-_]+)$");

        public bool IsPathFullyQualified(string path) => Regex.IsMatch(path, _PathFullyQualifiedRegex);
        

        public bool IsPathRooted(string path) => Regex.IsMatch(path, _PathRootedRegex);

        public string[] SplitPath(string path)
        {
            var delimiters = new char[] { DirectorySeperator, AltDirectorySeperator };
            return path.Split(delimiters);
        }

        /// <summary>
        /// Returns a generated regex string to check if a path is fully qualified. 
        /// Ex. when DirectorySeperator = \ And AltDirectorySeperator = /
        /// @"^(?:[a-zA-Z0-9\-]\:[\\|/]|\\\\|//)"
        /// </summary>
        /// <returns>The regex used to check whether a path is fully qualified or not.</returns>
        private string GeneratePathFullyQualifiedRegex()
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append(@"^(?:[a-zA-Z0-9\-]\:[");
            sb.Append($"{Regex.Escape(DirectorySeperator.ToString())}|{Regex.Escape(AltDirectorySeperator.ToString())}]|");
            sb.Append($"{Regex.Escape(DirectorySeperator.ToString())}{Regex.Escape(DirectorySeperator.ToString())}|");
            sb.Append($"{Regex.Escape(AltDirectorySeperator.ToString())}{Regex.Escape(AltDirectorySeperator.ToString())})");
            return sb.ToString();
        }

        /// <summary>
        /// Returns a generated regex string to check if a path is rooted. 
        /// Ex. when DirectorySeperator = \ And AltDirectorySeperator = /
        /// @"^(?:[a-zA-Z0-9\-]+\:|\\{1,2}|/{1,2})"
        /// </summary>
        /// <returns>The regex used to check whether a path is rooted or not.</returns>
        private string GeneratePathRootedRegex()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"^(?:[a-zA-Z0-9\-]+\:|");
            sb.Append(Regex.Escape(DirectorySeperator.ToString()));
            sb.Append(@"{1,2}|");
            sb.Append(Regex.Escape(AltDirectorySeperator.ToString()));
            sb.Append(@"{1,2})");
            return sb.ToString();
        }
    }
}

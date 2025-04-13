using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;
using System.Text;
using System.Text.RegularExpressions;

namespace FakeFileSystem.Services
{
    public class InMemoryPathService : IPathService
    {
        private readonly IFileSystem _fileSystem;

        private readonly string _PathRootedRegex;

        private readonly string _PathFullyQualifiedRegex;

        public char AltDirectorySeperator => _fileSystem.AltDirectorySeperator;

        public char DirectorySeperator => _fileSystem.DirectorySeperator;

        public InMemoryPathService(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
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
            return string.Join(_fileSystem.DirectorySeperator, pathParts);
        }

        public string CombinePath(string path1, string path2)
        {
            return $"{ path1}{_fileSystem.DirectorySeperator}{path2}";
        }

        public string CombinePath(string path1, string path2, string path3)
        {
            return $"{path1}{_fileSystem.DirectorySeperator}{path2}{_fileSystem.DirectorySeperator}{path3}";
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

        public bool HasExtension(string fileName)
        {
            return Regex.IsMatch(fileName, @"(?:\.[a-zA-Z0-9\-_]+)$");
        }

        public bool IsPathFullyQualified(string path)
        {
            return Regex.IsMatch(path, _PathFullyQualifiedRegex);
        }

        public bool IsPathRooted(string path)
        {
            return Regex.IsMatch(path, _PathRootedRegex);
        }

        public string[] SplitPath(string path)
        {
            var delimiters = new char[] { _fileSystem.DirectorySeperator, _fileSystem.AltDirectorySeperator };
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
            sb.Append($"{Regex.Escape(_fileSystem.DirectorySeperator.ToString())}|{Regex.Escape(_fileSystem.AltDirectorySeperator.ToString())}]|");
            sb.Append($"{Regex.Escape(_fileSystem.DirectorySeperator.ToString())}{Regex.Escape(_fileSystem.DirectorySeperator.ToString())}|");
            sb.Append($"{Regex.Escape(_fileSystem.AltDirectorySeperator.ToString())}{Regex.Escape(_fileSystem.AltDirectorySeperator.ToString())})");
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
            sb.Append(Regex.Escape(_fileSystem.DirectorySeperator.ToString()));
            sb.Append(@"{1,2}|");
            sb.Append(Regex.Escape(_fileSystem.AltDirectorySeperator.ToString()));
            sb.Append(@"{1,2})");
            return sb.ToString();
        }
    }
}

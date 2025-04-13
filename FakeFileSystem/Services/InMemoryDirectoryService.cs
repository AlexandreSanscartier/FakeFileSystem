using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Factories.FileSystems;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Models.FileSystems;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models.FileSystems;
using System.Reflection.Metadata.Ecma335;

namespace FakeFileSystem.Services
{
    public class InMemoryDirectoryService : IDirectoryService
    {
        private string _currentDirectory;

        private IDirectoryComponent _currentDirectoryFileSystemComponent;

        private readonly IDirectoryComponentFactory _directoryComponentFactory;

        private readonly IDirectoryInfoFactory _directoryInfoFactory;

        private readonly IPathService _pathService;

        private readonly IFileSystem _fileSystem;

        public InMemoryDirectoryService(IDirectoryComponentFactory directoryComponentFactory, IPathService pathService, 
            IFileSystem fileSystem, IDirectoryInfoFactory directoryInfoFactory)
        {
            _directoryComponentFactory = directoryComponentFactory;
            _pathService = pathService;
            _fileSystem = fileSystem;
            _directoryInfoFactory = directoryInfoFactory;
            _currentDirectoryFileSystemComponent = _fileSystem.Root;
            _currentDirectory = $"{_fileSystem.Root.Name}{_pathService.DirectorySeperator}";
        }

        public IDirectoryInfo CreateDirectory(string path)
        {
            var pathParts = _pathService.SplitPath(path);
            var directoryName = pathParts.Last();
            IDirectoryComponent directory;
            if (pathParts.Length == 1)
                directory = _currentDirectoryFileSystemComponent;
            else
            {
                var currentPath = string.Empty;
                foreach (var pathPart in pathParts)
                {
                    currentPath += $"{pathPart}{_pathService.DirectorySeperator}" ?? string.Empty;
                    if (!this.DirectoryExists(currentPath))
                    {
                        var currentPathParts = _pathService.SplitPath(currentPath).SkipLast(1);
                        directory = FindDirectory(_pathService.CombinePath(currentPathParts.SkipLast(1).ToArray())).DirectoryComponent;
                        var directoryToAddRecursivelyAdd = _directoryComponentFactory.Create(currentPathParts.Last());
                        directory.Add(directoryToAddRecursivelyAdd);
                    }
                }
                directory = FindDirectory(_pathService.CombinePath(pathParts.SkipLast(1).ToArray())).DirectoryComponent;
            }

            var directoryToAdd = _directoryComponentFactory.Create(directoryName);
            directory.Add(directoryToAdd);

            return _directoryInfoFactory.Create(directoryToAdd);
        }

        public void DeleteDirectory(string path)
        {
            DeleteDirectory(path, false);
        }

        public void DeleteDirectory(string path, bool recursive)
        {
            var directoryInfo = FindDirectory(path);

            if (directoryInfo is null || directoryInfo.DirectoryComponent is null)
                throw new DirectoryNotFoundException($"{path} does not exist");

            var directory = directoryInfo.DirectoryComponent;

            var parentDirectory = directory.Parent;
            if (recursive || !directory.GetFileSystemComponents().Any())
                parentDirectory?.Remove(directory);
        }

        public bool DirectoryExists(string path)
        {
            var foundDirectory = FindDirectory(path);
            return foundDirectory is not null;
        }

        public string GetCurrentDirectory() => _currentDirectory;

        public IDirectoryInfo GetParent(string path)
        {
            var directoryInfo = FindDirectory(path);
            var directory = directoryInfo.DirectoryComponent;
            return new InMemoryDirectoryInfo(directory.Parent);
        }

        public IEnumerable<string> GetFiles(string path)
        {
            var directoryInfo = FindDirectory(path);

            if (directoryInfo is null || directoryInfo.DirectoryComponent is null)
                throw new DirectoryNotFoundException($"{path} does not exist");

            var directory = directoryInfo.DirectoryComponent;


            if (!directory.GetFileSystemComponents().Any(x => IsFile(x)))
                return new string[0];

            /// TODO Return fully qualified paths instead of names
            return directory.GetFileSystemComponents().Where(x => IsFile(x)).Select(x => x.Name);
        }

        public IDirectoryInfo GetDirectory(string path)
        {
            var directoryInfo = FindDirectory(path);
            var directory = directoryInfo?.DirectoryComponent;

            if (directory is null)
                throw new DirectoryNotFoundException($"{path} does not exist");

            return _directoryInfoFactory.Create(directory);
        }

        public IEnumerable<string> GetDirectories(string path)
        {
            var directoryInfo = FindDirectory(path);

            if (directoryInfo is null || directoryInfo.DirectoryComponent is null)
                throw new DirectoryNotFoundException($"{path} does not exist");

            var directory = directoryInfo.DirectoryComponent;

            if (directory is null)
                throw new DirectoryNotFoundException($"{path} does not exist");

            if(!directory.GetFileSystemComponents().Any(x => IsDirectory(x)))
                return new string[0];

            /// TODO Return fully qualified paths instead of names
            return directory.GetFileSystemComponents().Where(x => IsDirectory(x)).Select(x => x.Name);
        }

        public void SetCurrentDirectory(string path)
        {
            /// TODO: DirectoryExists already calls FindDirectory and it's a costly operation perhaps just do the check here
            if (!DirectoryExists(path))
                throw new DirectoryNotFoundException($"{path} does not exist");

            var directoryInfo = FindDirectory(path);
            _currentDirectoryFileSystemComponent = directoryInfo.DirectoryComponent;
            _currentDirectory = directoryInfo.FullPath;
        }

        private IDirectoryInfo? FindDirectory(string path)
        {
            var pathParts = _pathService.SplitPath(path).Where(x => !x.Equals(string.Empty));

            var currentDirectory = _pathService.IsPathRooted(path) ? _fileSystem.Root : _currentDirectoryFileSystemComponent;
            try
            {
                foreach (var pathPart in pathParts)
                {
                    if(_pathService.IsPathRooted(pathPart))
                        continue;
                    currentDirectory = RecursiveFindDirectory(pathPart, currentDirectory);
                }
                return _directoryInfoFactory.Create(currentDirectory);
            }
            catch(DirectoryNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
            }
            /// TODO: Potentially return a Null Object instead of null.
            return null;
        }

        private IDirectoryComponent RecursiveFindDirectory(string directoryName, IDirectoryComponent current)
        {
            /// TODO Measure the effectiveness of this operation and optimize if needed.
            var subDirectories = current.GetFileSystemComponents().Where(fsc => IsDirectory(fsc));
            var directory = subDirectories.Where(sd => sd.Name.Equals(directoryName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            /// TODO: Add the full path to the exception for debugging purposes.
            if (directory is null)
                throw new DirectoryNotFoundException($"{directoryName} Not found");

            return (IDirectoryComponent)directory;
        }

        private bool IsDirectory(IFileSystemComponent fileSystemComponent)
        {
            return fileSystemComponent.GetType().IsAssignableTo(typeof(IDirectoryComponent));
        }

        private bool IsFile(IFileSystemComponent fileSystemComponent)
        {
            return fileSystemComponent.GetType().IsAssignableTo(typeof(IFileComponent));
        }
    }
}

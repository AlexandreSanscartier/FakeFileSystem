using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Interfaces.Models.FileSystems;

namespace FakeFileSystem.Services
{
    public class InMemoryFileService : IFileService
    {
        private readonly IFileComponentFactory _fileComponentFactory;

        private readonly IMemoryStreamFactory _memoryStreamFactory;

        private readonly IDirectoryService _directoryService;

        private readonly IPathService _pathService;

        private readonly IFilePathFactory _filePathFactory;

        public InMemoryFileService(IFileComponentFactory fileComponentFactory, IMemoryStreamFactory memoryStreamFactory,
            IDirectoryService directoryService, IPathService pathService, IFilePathFactory filePathFactory)
        {
            _fileComponentFactory = fileComponentFactory;
            _memoryStreamFactory = memoryStreamFactory;
            _directoryService = directoryService;
            _pathService = pathService;
            _filePathFactory = filePathFactory;
        }

        public Stream CreateFile(string path)
        {
            var filePath = SeperateFolderFromFileName(path);
            var directoryInfo = _directoryService.GetDirectory(filePath.Path);

            var file = _fileComponentFactory.Create(filePath.FileName);
            directoryInfo.DirectoryComponent.Add(file);

            return _memoryStreamFactory.Create(string.Empty);
        }

        public void DeleteFile(string path)
        {
            var filePath = SeperateFolderFromFileName(path);
            var directoryInfo = _directoryService.GetDirectory(filePath.Path);

            var file = ExtractFileFromDirectory(directoryInfo, filePath.FileName);

            if (file is null)
                throw new NullReferenceException($"File {path} does not exist.");

            directoryInfo.DirectoryComponent.Remove(file);
        }

        public bool FileExists(string path)
        {
            var filePath = SeperateFolderFromFileName(path);
            /// TODO: Paramaterize whether file names should be ignore case or not.
            return _directoryService.GetFiles(filePath.Path).Any(x => x.Equals(filePath.FileName, StringComparison.OrdinalIgnoreCase));
        }

        public string ReadAllText(string path)
        {
            var filePath = SeperateFolderFromFileName(path);
            var directoryInfo = _directoryService.GetDirectory(filePath.Path);

            var file = ExtractFileFromDirectory(directoryInfo, filePath.FileName);


            return file.Content;
        }

        public void WriteAllText(string path, string contents)
        {
            var filePath = SeperateFolderFromFileName(path);
            var directoryInfo = _directoryService.GetDirectory(filePath.Path);

            if(!this.FileExists(path))
            {
                this.CreateFile(path);
            }

            var file = ExtractFileFromDirectory(directoryInfo, filePath.FileName);
            file.Content = contents;
        }

        private IFileComponent ExtractFileFromDirectory(IDirectoryInfo directoryInfo, string fileName)
        {
            var directoryComponent = directoryInfo.DirectoryComponent;
            var fileSystemComponents = directoryComponent.GetFileSystemComponents();
            /// TODO: Paramaterize whether file names should be ignore case or not.
            var fileComponent = fileSystemComponents.First(x => x.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));
            return (IFileComponent)fileComponent;
        }

        private IFilePath SeperateFolderFromFileName(string path)
        {
            var pathParts = _pathService.SplitPath(path);

            var folder = _pathService.CombinePath(pathParts.SkipLast(1).ToArray());
            var fileName = pathParts.Last();
            return _filePathFactory.Create(folder, fileName);
        }
    }
}

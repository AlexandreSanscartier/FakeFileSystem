using FakeFileSystem.CLI.Interfaces.Commands.Directories;
using FakeFileSystem.Interfaces.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Directories
{
    public sealed class SetCurrentDirectoryCommand : IDirectoryCommand
    {
        private readonly string _path;

        private IDirectoryService _directoryService;

        private IPathService _pathService;

        public SetCurrentDirectoryCommand(IDirectoryService directoryService, IPathService pathService, string path)
        {
            _directoryService = directoryService;
            _pathService = pathService;
            _path = path;
        }

        public void Execute()
        {
            var path = _path;
            if (CanNavigateBack(path))
                NavigateToPreviousDirectory(path);

            if (CanAttemptToNavigateForward(path))
                path = NavigateToDirectory(path);
        }

        private bool CanNavigateBack(string path)
        {
            var pathParts = _pathService.SplitPath(path);
            return pathParts.Last().Equals("..") && pathParts.Length >= 3;
        }

        private bool CanAttemptToNavigateForward(string path)
        {
            var pathParts = _pathService.SplitPath(path);
            return !pathParts.Last().Equals("..");
        }

        private string NavigateToDirectory(string path)
        {
            if (!_pathService.EndsInDirectorySeperator(_path))
                path += _pathService.DirectorySeperator;

            _directoryService.SetCurrentDirectory(path);
            return path;
        }

        private void NavigateToPreviousDirectory(string path)
        {
            var pathParts = _pathService.SplitPath(path);
            var parentPath = _pathService.CombinePath(pathParts.SkipLast(2).ToArray());
            _directoryService.SetCurrentDirectory(parentPath);
        }
    }
}

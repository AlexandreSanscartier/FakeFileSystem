using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models;
using Moq;
using System.Collections.Generic;

namespace FakeFileSystem.Unit.Tests.Mocks
{
    public class FileSystemTestMock
    {
        private IDictionary<string, IDirectoryComponent> _directoryComponentDictionary;
        private readonly IPathService _pathService;

        public FileSystemTestMock()
        {
            _directoryComponentDictionary = new Dictionary<string, IDirectoryComponent>();
            _pathService = Mock.Of<IPathService>();
        }

        /*
         * C:\FakeDir1
         *      - FakeDir2
         *          - FakeDir3
         *      - FakeDir2A
         *      - FakeDir2B
         *      - FakeDir2C
         *      - FakeFile.txt
         */
        public void InitializeBasicFileSystem()
        {
            var root = new DirectoryComponent(_pathService, @"C:");
            var level1 = new DirectoryComponent(_pathService, "FakeDir");
            var level2 = new DirectoryComponent(_pathService, "FakeDir2");
            var level2a = new DirectoryComponent(_pathService, "FakeDir2A");
            var level2b = new DirectoryComponent(_pathService, "FakeDir2B");
            var level2c = new DirectoryComponent(_pathService, "FakeDir2C");
            var level2File = new FileComponent(_pathService, "FakeFile.txt", "Fake content");
            var level3 = new DirectoryComponent(_pathService, "FakeDir3");
            level1.Add(level2);
            level1.Add(level2a);
            level1.Add(level2b);
            level1.Add(level2c);
            level1.Add(level2File);
            level2.Add(level3);
            root.Add(level1);

            _directoryComponentDictionary.Add("root", root);
            _directoryComponentDictionary.Add("FakeDir", level1);
            _directoryComponentDictionary.Add("FakeDir2", level2);
            _directoryComponentDictionary.Add("FakeDir3", level3);
        }
    }
}

using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Models;
using System.Collections.Generic;

namespace FakeFileSystem.Unit.Tests.Mocks
{
    public class FileSystemTestMock
    {
        private IDictionary<string, IDirectoryComponent> _directoryComponentDictionary;

        public FileSystemTestMock()
        {
            _directoryComponentDictionary = new Dictionary<string, IDirectoryComponent>();
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
            var root = DirectoryComponent.From(@"C:");
            var level1 = DirectoryComponent.From("FakeDir");
            var level2 = DirectoryComponent.From("FakeDir2");
            var level2a = DirectoryComponent.From("FakeDir2A");
            var level2b = DirectoryComponent.From("FakeDir2B");
            var level2c = DirectoryComponent.From("FakeDir2C");
            var level2File = new FileComponent("FakeFile.txt", "Fake content");
            var level3 = DirectoryComponent.From("FakeDir3");
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

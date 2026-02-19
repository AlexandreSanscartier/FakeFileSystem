using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FakeFileSystem.Unit.Tests.Models
{
    public class DirectoryComponentTests
    {
        private Mock<IPathService> _pathServiceMock;

        public DirectoryComponentTests()
        {
            _pathServiceMock = new Mock<IPathService>();
        }

        [Fact]
        public void AddFileSystemComponent_AddsSuccessfully()
        {
            // Arrange
            var pathService = _pathServiceMock.Object;
            var componentToAdd = new DirectoryComponent(pathService, "FakeDirectory");
            var directoryComponent = new DirectoryComponent(pathService, @"C:");
            var expected = new List<IFileSystemComponent>() { componentToAdd };

            // Act
            directoryComponent.Add(componentToAdd);
            var actual = directoryComponent.GetFileSystemComponents();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddFileSystemComponent_WhenAlreadyExists_DoesNotAdd()
        {
            // Arrange
            var pathService = _pathServiceMock.Object;
            var componentToAdd = new DirectoryComponent(pathService, "FakeDirectory");
            var directoryComponent = new DirectoryComponent(pathService, @"C:");
            var expected = new List<IFileSystemComponent>() { componentToAdd };
            var expectedCount = 1;

            // Act
            directoryComponent.Add(componentToAdd);
            var actual = directoryComponent.GetFileSystemComponents();

            // Assert
            Assert.Equal(expected, actual);
            Assert.Equal(expectedCount, actual.Count());
        }

        [Fact]
        public void AddFileSystemComponent_SetsParent()
        {
            // Arrange
            var pathService = _pathServiceMock.Object;
            var componentToAdd = new DirectoryComponent(pathService, "FakeDirectory");
            var directoryComponent = new DirectoryComponent(pathService, @"C:");
            var expected = directoryComponent;

            // Act
            directoryComponent.Add(componentToAdd);
            var actual = componentToAdd.Parent;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}

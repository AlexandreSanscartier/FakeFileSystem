using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FakeFileSystem.Unit.Tests.Models
{
    public class DirectoryComponentTests
    {
        [Fact]
        public void AddFileSystemComponent_AddsSuccessfully()
        {
            // Arrange
            var componentToAdd = DirectoryComponent.From("FakeDirectory");
            var directoryComponent = DirectoryComponent.From(@"C:");
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
            var componentToAdd = DirectoryComponent.From("FakeDirectory");
            var directoryComponent = DirectoryComponent.From(@"C:");
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
            var componentToAdd = DirectoryComponent.From("FakeDirectory");
            var directoryComponent = DirectoryComponent.From(@"C:");
            var expected = directoryComponent;

            // Act
            directoryComponent.Add(componentToAdd);
            var actual = componentToAdd.Parent;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}

using FakeFileSystem.CLI.ConsoleLibrary.Commands.Factories;
using FakeFileSystem.CLI.ConsoleLibrary.Commands.Models;
using FakeFileSystem.CLI.ConsoleLibrary.Commands.Services;
using FakeFileSystem.CLI.ConsoleLibrary.Components;
using FakeFileSystem.CLI.ConsoleLibrary.Services;
using FakeFileSystem.CLI.ConsoleLibrary.Utilities;
using FakeFileSystem.CLI.Interfaces.Commands.Factories;
using FakeFileSystem.CLI.Interfaces.Commands.Models;
using FakeFileSystem.CLI.Interfaces.Commands.Services;
using FakeFileSystem.CLI.Interfaces.Components;
using FakeFileSystem.CLI.Interfaces.Services;
using FakeFileSystem.CLI.Interfaces.Utilities;
using FakeFileSystem.Factories;
using FakeFileSystem.Factories.FileSystems;
using FakeFileSystem.Interfaces;
using FakeFileSystem.Interfaces.Factories;
using FakeFileSystem.Interfaces.Factories.FileSystems;
using FakeFileSystem.Interfaces.Models;
using FakeFileSystem.Interfaces.Models.FileSystems;
using FakeFileSystem.Interfaces.Services;
using FakeFileSystem.Models;
using FakeFileSystem.Models.FileSystems;
using FakeFileSystem.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FakeFileSystem.CLI.ConsoleLibrary
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterConsoleLibrary(this IServiceCollection services)
        {
            services.AddSingleton<IInputReader, ConsoleInputReader>();
            services.AddSingleton<IOutputWriter, ConsoleOutputWriter>();

            services.AddTransient<IDirectoryCommandParameters, DirectoryCommandParameters>();
            services.AddTransient<IFileCommandParameters, FileCommandParameters>();
            services.AddTransient<IConsoleCommandParameters, ConsoleCommandParameters>();

            services.AddSingleton<ICommandRunner, FileSystemCommandRunner>();
            services.AddTransient<IHeaderComponent, ConsoleHeaderComponent>();

            services.AddSingleton<IFFSystem, InMemoryFFileSystem>();
            services.AddSingleton<IFFSystemService, FFSystemService>();

            services.AddTransient<IDirectoryComponentFactory, DirectoryComponentFactory>();
            services.AddTransient<IFileComponentFactory, FileComponentFactory>();
            services.AddTransient<IMemoryStreamFactory, MemoryStreamFactory>();
            services.AddTransient<IFilePathFactory, FilePathFactory>();
            services.AddTransient<IDirectoryInfoFactory, InMemoryDirectoryInfoFactory>();

            services.AddTransient<IDirectoryCommandFactory, DirectoryCommandFactory>();
            services.AddTransient<IFileCommandFactory, FileCommandFactory>();
            services.AddTransient<IConsoleCommandFactory, ConsoleCommandFactory>();

            services.AddSingleton<IPathService, InMemoryPathService>();
            services.AddSingleton<IDirectoryService, InMemoryDirectoryService>();
            services.AddSingleton<IFileService, InMemoryFileService>();

            services.AddTransient<IFileSystemDirectorySeperator>(x => new FileSystemDirectorySeperator('\\', '/'));
            services.AddTransient<IFileSystem, FileSystem>();

            return services;
        }
    }
}

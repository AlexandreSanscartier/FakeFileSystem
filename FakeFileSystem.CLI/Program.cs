using FakeFileSystem.CLI.ConsoleLibrary;
using FakeFileSystem.CLI.ConsoleLibrary.Commands;
using FakeFileSystem.CLI.ConsoleLibrary.Commands.Consoles;
using FakeFileSystem.CLI.Interfaces.Commands;
using FakeFileSystem.CLI.Interfaces.Commands.Factories;
using FakeFileSystem.CLI.Interfaces.Components;
using FakeFileSystem.CLI.Interfaces.Services;
using FakeFileSystem.CLI.Interfaces.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new ConfigurationBuilder();
var configurationRoot = builder.Build();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
            .RegisterConsoleLibrary()
    ).Build();

using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;

var consoleOutputWriter = provider.GetRequiredService<IOutputWriter>();
var consoleCommandFactory = provider.GetRequiredService<IConsoleCommandFactory>();
var headerComponent = provider.GetRequiredService<IHeaderComponent>();

ICommand command = new NullCommand();
var input = string.Empty;
var quit = false;

headerComponent.PrintHeader("FakeFileSystem.CLI");

var fakeFileSystem = provider.GetRequiredService<IFFSystemService>().FakeFileSystem;

var commandDictionary = new Dictionary<string, Type>
{
    { "touch", typeof(TouchConsoleCommand) },
    { "cd", typeof(ChangeDirectoryConsoleCommand) },
    { "mkdir", typeof(CreateDirectoryConsoleCommand) },
    { "rmdir", typeof(RemoveDirectoryConsoleCommand) },
    { "ls", typeof(ListFilesFoldersConsoleCommand) },
    { "dir", typeof(ListFilesFoldersConsoleCommand) },
    { "write", typeof(WriteToFileConsoleCommand) },
    { "read", typeof(ReadFromFileConsoleCommand) },
    { "pwd", typeof(PresentWorkingDirectoryConsoleCommand) }
};

do
{
    var currentDirectory = fakeFileSystem.DirectoryService.GetCurrentDirectory();
    currentDirectory = currentDirectory.Equals("C:") ? $"{currentDirectory}{fakeFileSystem.PathService.DirectorySeperator}" : currentDirectory;
    consoleOutputWriter.Write($"{currentDirectory}>");
    input = Console.ReadLine();
    if (!string.IsNullOrEmpty(input))
    {
        var inputParts = input.Split(" ");
        var argument = string.Join(" ", inputParts.Skip(1));

        if (commandDictionary.TryGetValue(inputParts[0], out Type? consoleCommandType))
        {
            consoleCommandFactory.SetArguments(argument);
            command = consoleCommandFactory.Create(consoleCommandType);
            command.Execute();
        }

        switch (inputParts[0])
        {
            case "q":
                quit = true;
                break;
            default:
                continue;
        }
    }
} while (!quit);
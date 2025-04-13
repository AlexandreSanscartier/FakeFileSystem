using FakeFileSystem.CLI.Interfaces.Commands;
using FakeFileSystem.CLI.Interfaces.Commands.Factories;
using FakeFileSystem.CLI.ConsoleLibrary.Commands;
using FakeFileSystem.CLI.ConsoleLibrary.Commands.Factories;
using FakeFileSystem.CLI.ConsoleLibrary.Commands.Models;
using FakeFileSystem.CLI.ConsoleLibrary.Commands.Services;
using FakeFileSystem.CLI.ConsoleLibrary.Services;
using FakeFileSystem.CLI.ConsoleLibrary.Utilities;
using FakeFileSystem.CLI.ConsoleLibrary.Components;
using FakeFileSystem.CLI.ConsoleLibrary.Commands.Consoles;

var fakeFileSystem = new FFSystemService().FakeFileSystem;
var quit = false;

var input = string.Empty;

var consoleInputReader = new ConsoleInputReader();
var consoleOutputWriter = new ConsoleOutputWriter();

var headerComponent = new ConsoleHeaderComponent(consoleOutputWriter);
headerComponent.PrintHeader("FakeFileSystem.CLI");

var directoryCommandParameters = new DirectoryCommandParameters(
    fakeFileSystem.DirectoryService, fakeFileSystem.PathService, string.Empty, string.Empty
);

var fileCommandParameters = new FileCommandParameters(
    fakeFileSystem.FileService, string.Empty, string.Empty
);

IDirectoryCommandFactory directoryCommandFactory = new DirectoryCommandFactory(directoryCommandParameters);
IFileCommandFactory fileCommandFactory = new FileCommandFactory(fileCommandParameters);
var commandRunner = new FileSystemCommandRunner();

var consoleCommandParameters = new ConsoleCommandParameters(
    consoleInputReader, consoleOutputWriter, fakeFileSystem, directoryCommandFactory, fileCommandFactory, commandRunner
);

IConsoleCommandFactory consoleCommandFactory = new ConsoleCommandFactory(consoleCommandParameters);
ICommand command = new NullCommand();

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

        if (commandDictionary.ContainsKey(inputParts[0]))
        {
            var consoleCommandType = commandDictionary[inputParts[0]];
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
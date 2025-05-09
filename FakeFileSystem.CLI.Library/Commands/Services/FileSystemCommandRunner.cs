﻿using FakeFileSystem.CLI.Interfaces.Commands;
using FakeFileSystem.CLI.Interfaces.Commands.Services;

namespace FakeFileSystem.CLI.ConsoleLibrary.Commands.Services
{
    public class FileSystemCommandRunner : ICommandRunner
    {
        public void Execute(ICommand command)
        {
            command.Execute();
        }

        public T Execute<T>(ICommand command)
        {
            command.Execute();
            if (command.GetType().IsAssignableTo(typeof(ICommandWithResult<T>)))
                return ((ICommandWithResult<T>)command).Result;

            throw new Exception();
            //return default;
        }
    }
}

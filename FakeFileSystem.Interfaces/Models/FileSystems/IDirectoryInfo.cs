namespace FakeFileSystem.Interfaces.Models.FileSystems
{
    // TODO: Eventually subclass this to mimick the actual DirectoryInfo class https://docs.microsoft.com/en-us/dotnet/api/system.io.filesysteminfo?view=net-6.0
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.directoryinfo?view=net-6.0 is a sealed class so we will need to mimic as close as possible
    public interface IDirectoryInfo
    {
        IDirectoryComponent DirectoryComponent { get; }

        string FullPath { get; }
    }
}

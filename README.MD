<!-- https://dillinger.io/ for modifying the MD file and viewing changes in realtime -->

# FakeFileSystem
## _When you want to create an in memory filesystem... for tests... in C#_
What do you get when you run into a problem that you could solve with an existing library, but decide you would rather have some fun and code it yourself? You get the FakeFileSystem, a virtual filesystem that lives in memory for testing code that requires access to a filesystem.

## Should you use this library?
Probably not, but feel free to log any issues you have and I'll get to them as soon as I can. 

## What problem does this solve?

- You want to write unit tests for functions that manipulate folders / files.
- You want to write integration tests, but don't want to have to clean up folders / files created on disk.
- You want to write a CLI that interacts with a virtual file system, but don't have time to write your own.
- I was looking for a project to overengineer and this seemed like a good candidate.

#### Is the library feature complete?
**Great question!**
At the moment the library is not feature complete. I wrote what I needed and then left the rest for now as I had what I needed. I intend to add more features whenever I have free time. 

```csharp
/* Example usage of the IDirectoryService */
var fileSystem = new InMemoryFFileSystem(); // Create an instance of the InMemoryFFileSystem class.
var directoryName = "MyDirectory";
fileSystem.DirectoryServices.CreateDirectory(directoryName); // Creates a new directory called "MyDirectory"
fileSystem.DirectoryServices.DeleteDirectory(directoryName); // Deletes the "MyDirectory" directory
```
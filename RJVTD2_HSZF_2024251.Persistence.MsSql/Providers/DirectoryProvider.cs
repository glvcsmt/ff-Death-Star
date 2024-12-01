namespace RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

// Interface defining operations related to directory management
public interface IDirectoryProvider
{
    // Ensures that a directory exists
    public bool EnsureDirectoryExists(string directoryName);
    
    // Creates a directory if it does not already exist

    public bool CreateDirectory(string directoryName);
    
    // Reads the content of a specified directory

    public IEnumerable<FileSystemInfo> ReadDirectoryContent(string directoryName);
    
    // Deletes the specified directory

    public bool DeleteDirectory(string directoryName);
}

// Implementation of the IDirectoryProvider interface
// This class provides methods to interact with the file system, 
// such as creating, reading, and deleting directories.
public class DirectoryProvider : IDirectoryProvider
{
    // Base directory path for the directories, relative to the application's location
    private string _basePath = "../../../../Shipments";
    
    // Checks if the directory exists at the specified path
    public bool EnsureDirectoryExists(string directoryName)
    {
        // Combines the base path with the directory name and checks if the directory exists
        return Directory.Exists(directoryName);
    }

    // Creates a new directory if it doesn't already exist
    public bool CreateDirectory(string directoryName)
    {
        // If the directory doesn't exist, it will be created and return true
        if (!EnsureDirectoryExists(Path.Combine(_basePath, directoryName)))
        {
            Directory.CreateDirectory(Path.Combine(_basePath, directoryName));
            return true;
        }
        // If the directory already exists, return false
        else return false;
    }

    // Reads the content of a directory (files and subdirectories)
    public IEnumerable<FileSystemInfo> ReadDirectoryContent(string directoryName)
    {
        // If the directory exists, return its contents (files and subdirectories)
        if (EnsureDirectoryExists(Path.Combine(_basePath, directoryName)))
        {
            return new DirectoryInfo(Path.Combine(_basePath, directoryName)).GetFileSystemInfos();
        }
        // If the directory doesn't exist, throw a DirectoryNotFoundException
        else
        {
            throw new DirectoryNotFoundException();
        } 
    }

    // Deletes a directory and its contents (if it exists)
    public bool DeleteDirectory(string directoryName)
    {
        // If the directory exists, delete it and its contents, then return true
        if (EnsureDirectoryExists(Path.Combine(_basePath, directoryName)))
        {
            Directory.Delete(Path.Combine(_basePath, directoryName), true);
            return true;
        }
        // If the directory doesn't exist, return false
        else return false;
    }
}
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Application.Services;

// Interface defining operations for directory management in the service layer
public interface IDirectoryService
{
    // Method to check if a directory exists
    public bool EnsureDirectoryExists(string directoryName);
    
    // Method to create a new directory
    public bool CreateDirectory(string directoryName);
    
    // Method to read the content of a directory
    public IEnumerable<FileSystemInfo> ReadDirectoryContent(string directoryName);
    
    // Method to delete a directory
    public bool DeleteDirectory(string directoryName);
}

// Implementation of the IDirectoryService interface
// This service interacts with the IDirectoryProvider to manage directories
public class DirectoryService : IDirectoryService
{
    // Private field for the injected IDirectoryProvider dependency
    private readonly IDirectoryProvider _directoryProvider;
    
    // Constructor that accepts an IDirectoryProvider instance for dependency injection
    public DirectoryService(IDirectoryProvider directoryProvider)
    {
        // Initialize the _directoryProvider field with the injected provider instance
        _directoryProvider = directoryProvider;
    }
    
    // Ensures that the specified directory exists, delegates to IDirectoryProvider
    public bool EnsureDirectoryExists(string directoryName)
    {
        return _directoryProvider.EnsureDirectoryExists(directoryName);
    }

    // Creates a new directory, delegates to IDirectoryProvider
    public bool CreateDirectory(string directoryName)
    {
        return _directoryProvider.CreateDirectory(directoryName);
    }

    // Retrieves the content of the specified directory, delegates to IDirectoryProvider
    public IEnumerable<FileSystemInfo> ReadDirectoryContent(string directoryName)
    {
        return _directoryProvider.ReadDirectoryContent(directoryName);
    }

    // Deletes the specified directory, delegates to IDirectoryProvider
    public bool DeleteDirectory(string directoryName)
    {
        return _directoryProvider.DeleteDirectory(directoryName);
    }
}
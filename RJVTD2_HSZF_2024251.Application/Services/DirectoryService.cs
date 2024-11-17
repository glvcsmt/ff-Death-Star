using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Application.Services;

public interface IDirectoryService
{
    public bool EnsureDirectoryExists(string directoryName);
    
    public bool CreateDirectory(string directoryName);
    
    public IEnumerable<FileSystemInfo> ReadDirectoryContent(string directoryName);
    
    public bool DeleteDirectory(string directoryName);
}

public class DirectoryService : IDirectoryService
{
    public bool EnsureDirectoryExists(string directoryName)
    {
        return new DirectoryProvider().EnsureDirectoryExists(directoryName);
    }

    public bool CreateDirectory(string directoryName)
    {
        return new DirectoryProvider().CreateDirectory(directoryName);
    }

    public IEnumerable<FileSystemInfo> ReadDirectoryContent(string directoryName)
    {
        return new DirectoryProvider().ReadDirectoryContent(directoryName);
    }

    public bool DeleteDirectory(string directoryName)
    {
        return new DirectoryProvider().DeleteDirectory(directoryName);
    }
}
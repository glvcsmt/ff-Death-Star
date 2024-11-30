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
    private readonly IDirectoryProvider _directoryProvider;
    public DirectoryService(IDirectoryProvider directoryProvider)
    {
        _directoryProvider = directoryProvider;
    }
    public bool EnsureDirectoryExists(string directoryName)
    {
        return _directoryProvider.EnsureDirectoryExists(directoryName);
    }

    public bool CreateDirectory(string directoryName)
    {
        return _directoryProvider.CreateDirectory(directoryName);
    }

    public IEnumerable<FileSystemInfo> ReadDirectoryContent(string directoryName)
    {
        return _directoryProvider.ReadDirectoryContent(directoryName);
    }

    public bool DeleteDirectory(string directoryName)
    {
        return _directoryProvider.DeleteDirectory(directoryName);
    }
}
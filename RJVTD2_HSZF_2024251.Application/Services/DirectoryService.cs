using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Application.Services;

public interface IDirectoryService
{
    public bool EnsureDirectoryExists();
    
    public bool CreateDirectory();
    
    public IEnumerable<FileSystemInfo> ReadDirectoryContent();
    
    public bool DeleteDirectory();
}

public class DirectoryService : IDirectoryService
{
    private DirectoryProvider _directoryProvider;

    public DirectoryService(string directoryName)
    {
        _directoryProvider = new DirectoryProvider(directoryName);
    }
    
    public bool EnsureDirectoryExists()
    {
        return _directoryProvider.EnsureDirectoryExists();
    }

    public bool CreateDirectory()
    {
        return _directoryProvider.CreateDirectory();
    }

    public IEnumerable<FileSystemInfo> ReadDirectoryContent()
    {
        return _directoryProvider.ReadDirectoryContent();
    }

    public bool DeleteDirectory()
    {
        return _directoryProvider.DeleteDirectory();
    }
}
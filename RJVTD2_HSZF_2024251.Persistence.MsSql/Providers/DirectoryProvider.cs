namespace RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

public interface IDirectoryProvider
{
    public bool EnsureDirectoryExists(string directoryName);
    
    public bool CreateDirectory(string directoryName);
    
    public IEnumerable<FileSystemInfo> ReadDirectoryContent(string directoryName);
    
    public bool DeleteDirectory(string directoryName);
}

public class DirectoryProvider : IDirectoryProvider
{
    private string _basePath = "../../../../Shipments";
    
    public bool EnsureDirectoryExists(string directoryName)
    {
        return Directory.Exists(Path.Combine(_basePath, directoryName));
    }

    public bool CreateDirectory(string directoryName)
    {
        if (!EnsureDirectoryExists(Path.Combine(_basePath, directoryName)))
        {
            Directory.CreateDirectory(Path.Combine(_basePath, directoryName));
            return true;
        }
        else return false;
    }

    public IEnumerable<FileSystemInfo> ReadDirectoryContent(string directoryName)
    {
        if (EnsureDirectoryExists(Path.Combine(_basePath, directoryName)))
        {
            return new DirectoryInfo(Path.Combine(_basePath, directoryName)).GetFileSystemInfos();
        }
        else
        {
            throw new DirectoryNotFoundException();
        } 
    }

    public bool DeleteDirectory(string directoryName)
    {
        if (EnsureDirectoryExists(Path.Combine(_basePath, directoryName)))
        {
            Directory.Delete(Path.Combine(_basePath, directoryName), true);
            return true;
        }
        else return false;
    }
}
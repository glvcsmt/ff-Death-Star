namespace RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

public interface IDirectoryProvider
{
    public bool EnsureDirectoryExists();
    
    public bool CreateDirectory();
    
    public IEnumerable<FileSystemInfo> ReadAllDirectoryContent();
    
    public bool DeleteDirectory();
}

public class DirectoryProvider : IDirectoryProvider
{
    private string _basePath = "../../../../Shipments";

    public DirectoryProvider(string directory)
    {
        _basePath = Path.Combine(_basePath, directory);
    }
    public bool EnsureDirectoryExists()
    {
        return Directory.Exists(_basePath);
    }

    public bool CreateDirectory()
    {
        if (!EnsureDirectoryExists())
        {
            Directory.CreateDirectory(_basePath);
            return true;
        }
        else return false;
    }

    public IEnumerable<FileSystemInfo> ReadAllDirectoryContent()
    {
        if (EnsureDirectoryExists())
        {
            return new DirectoryInfo(_basePath).GetFileSystemInfos();
        }
        else
        {
            Console.WriteLine($"Directory not found: {_basePath}");
            return new List<FileSystemInfo>();
        } 
    }

    public bool DeleteDirectory()
    {
        throw new NotImplementedException();
    }
}
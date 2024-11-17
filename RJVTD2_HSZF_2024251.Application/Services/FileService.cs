using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Application.Services;

public interface IFileService
{
    public bool WrinteToFile<T>(List<T> content);
    
    public bool WriteToFile(string content);
    
    public string ReadFromFile();
    
    public bool DeleteFile();
}

public class FileService : IFileService
{
    private FileProvider _fileProvider;

    public FileService(string fileName)
    {
        _fileProvider = new FileProvider(fileName);
    }
    
    public bool WrinteToFile<T>(List<T> content)
    {
        return _fileProvider.WriteToFile(content);
    }

    public bool WriteToFile(string content)
    {
        return _fileProvider.WriteToFile(content);
    }

    public string ReadFromFile()
    {
        return _fileProvider.ReadFromFile();
    }

    public bool DeleteFile()
    {
        return _fileProvider.DeleteFile();
    }
}
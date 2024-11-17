using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Application.Services;

public interface IFileService
{
    public bool WrinteToFile<T>(string fileName, List<T> content);
    
    public bool WriteToFile(string fileName, string content);
    
    public string[] ReadFromFile(string fileName);
    
    public bool DeleteFile(string fileName);
}

public class FileService : IFileService
{
    public bool WrinteToFile<T>(string fileName, List<T> content)
    {
        return new FileProvider().WriteToFile(fileName, content);
    }

    public bool WriteToFile(string fileName, string content)
    {
        return new FileProvider().WriteToFile(fileName, content);
    }

    public string[] ReadFromFile(string fileName)
    {
        return new FileProvider().ReadFromFile(fileName);
    }

    public bool DeleteFile(string fileName)
    {
        return new FileProvider().DeleteFile(fileName);
    }
}
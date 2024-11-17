namespace RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

public interface IFileProvider
{
    public bool WriteToFile<T>(List<T> content);
    
    public bool WriteToFile(string content);
    
    public string ReadFromFile();
    
    public bool DeleteFile();
}

//string fileName = Path.GetFileName(filePath);
public class FileProvider : IFileProvider
{
    private string _basePath = "../../../../Shipments";
    
    public FileProvider(string fileName)
    {
        _basePath = Path.Combine(_basePath, fileName);
    }
    
    public bool WriteToFile<T>(List<T> content)
    {
        try
        {
            StreamWriter sw = new StreamWriter(_basePath);
            for (int i = 0; i < content.Count; i++)
            {
                sw.WriteLine(content[i]?.ToString());
            }
            sw.Close();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        
    }

    public bool WriteToFile(string content)
    {
        try
        {
            File.WriteAllText(_basePath, content);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public string ReadFromFile()
    {
        return File.ReadAllText(_basePath);
    }

    public bool DeleteFile()
    {
        try
        {
            File.Delete(_basePath);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}
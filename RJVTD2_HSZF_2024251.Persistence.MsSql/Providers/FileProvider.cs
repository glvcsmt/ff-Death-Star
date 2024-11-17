namespace RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

public interface IFileProvider
{
    public bool WriteToFile<T>(string fileName, List<T> content);
    
    public bool WriteToFile(string fileName, string content);
    
    public string[] ReadFromFile(string fileName);
    
    public bool DeleteFile(string fileName);
}

//string fileName = Path.GetFileName(filePath);
public class FileProvider : IFileProvider
{
    private string _basePath = "../../../../Shipments";
    
    public bool WriteToFile<T>(string fileName, List<T> content)
    {
        try
        {
            StreamWriter sw = new StreamWriter(Path.Combine(_basePath, fileName));
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

    public bool WriteToFile(string fileName, string content)
    {
        try
        {
            File.WriteAllText(Path.Combine(_basePath, fileName), content);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public string[] ReadFromFile(string fileName)
    {
        return File.ReadAllLines(Path.Combine(_basePath, fileName));
    }

    public bool DeleteFile(string fileName)
    {
        try
        {
            File.Delete(Path.Combine(_basePath, fileName));
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}
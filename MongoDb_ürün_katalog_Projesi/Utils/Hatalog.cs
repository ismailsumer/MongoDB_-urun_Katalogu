namespace MongoDB_ürün_katalog_Projesi.Utils;

public class HataLog
{
    private readonly string _FolderPath = "Logs";
    private readonly string _FilePath;

    public HataLog()
    {
        LogControlOrCreate();
        DateTime date = DateTime.Now;
        _FilePath = $"{_FolderPath}/{date.Year}-{date.Month}-{date.Day}.txt";
    }

    public HataLog(string filePath)
    {
        _FilePath = $"{_FolderPath}/{filePath}.txt";
    }

    private void LogControlOrCreate()
    {
        if (!Directory.Exists(_FolderPath))
        {
            Directory.CreateDirectory(_FolderPath);
        }
    }
    public void WriteToLog(string content)
    {
        string line = Environment.NewLine;
        File.AppendAllText(_FilePath, $"{content}{line}");
        Console.WriteLine("Log Başarıyla Eklendi.");
#if WINDOWS
                Console.Beep(1000, 500);
#endif
    }

    public void DeleteLog()
    {
        if (File.Exists(_FilePath))
        {
            File.Delete(_FilePath);
        }
    }
    public List<string> ReadLog()
    {
        var list = new List<string>();
        if (File.Exists(_FilePath))
        {
            using var reader = new StreamReader(_FilePath);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                list.Add(line);
            }
        }
        return list;
    }
    public List<string> ListLog()
    {
        List<string> list = new List<string>();
        if (Directory.Exists(_FolderPath))
        {
            var files = Directory.GetFiles(_FolderPath);
            foreach (var file in files)
            {
                list.Add(Path.GetFileName(file));
            }
        }
        list.Sort();
        return list;
    }
 }    

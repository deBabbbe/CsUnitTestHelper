namespace TestHelper;

public class TempCreateFile : IDisposable
{
    private string _path;

    public TempCreateFile(string path, string content)
    {
        _path = path.ExpandEnv();
        File.WriteAllText(_path, content);
    }

    public void Dispose()
    {
        File.Delete(_path);
    }
}
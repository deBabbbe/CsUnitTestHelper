namespace TestHelper;

public class TempCreateDirectory : IDisposable
{
    private string _path;

    public TempCreateDirectory(string path)
    {
        _path = path;
        Directory.CreateDirectory(path);
    }

    public void Dispose()
    {
        Directory.Delete(_path);
    }
}
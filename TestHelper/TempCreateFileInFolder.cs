namespace TestHelper;

public class TempCreateFileInFolder : IDisposable
{
    private string _path;
    private TempCreateDirectory _folder;

    public TempCreateFileInFolder(string path, string content)
    {
        _path = path;
        using (_folder = new TempCreateDirectory(Path.GetDirectoryName(path)))
        {
                File.WriteAllText(path, content);
        }
    }

    public void Dispose()
    {
        File.Delete(_path);
    }
}
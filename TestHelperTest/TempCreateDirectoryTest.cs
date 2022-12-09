
using ClassExtensions;
using TestHelper;

namespace TestHelperTest;

public class TempCreateDirectoryTests
{
    [Test]
    public void TempCreateDirectoryTests_CreatesAndDeletesFolder()
    {
        var path = "%temp%/Dings";
        DirectoryAssert.DoesNotExist(path.ExpandEnv());
        using (new TempCreateDirectory(path))
        {
            DirectoryAssert.Exists(path.ExpandEnv());
        }
        DirectoryAssert.DoesNotExist(path.ExpandEnv());
    }
}

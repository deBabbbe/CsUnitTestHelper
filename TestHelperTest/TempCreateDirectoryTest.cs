
using ClassExtensions;
using TestHelper;

namespace TestHelperTest;

[TestFixture]
public class TempCreateDirectoryTests
{
    [Test]
    public void TempCreateDirectoryTests_CreatesAndDeletesFolder()
    {
        const string path = "%temp%/Dings";
        DirectoryAssert.DoesNotExist(path.ExpandEnv());
        using (new TempCreateDirectory(path))
        {
            DirectoryAssert.Exists(path.ExpandEnv());
        }
        DirectoryAssert.DoesNotExist(path.ExpandEnv());
    }
}

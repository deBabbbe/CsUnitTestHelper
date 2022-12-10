
using ClassExtensions;
using TestHelper;

namespace TestHelperTest;

public class TempCreateFileInFolderTests
{
    [Test]
    public void TempCreateFileInFolderTests_CreatesAndDeletesFileInFolder()
    {
        var folder = "%temp%/Test";
        var path = $"{folder}/Temp.txt";
        FileAssert.DoesNotExist(path.ExpandEnv());
        var expectedText = Guid.NewGuid().ToString();
        using (new TempCreateFileInFolder(path, expectedText))
        {
            FileAssert.Exists(path.ExpandEnv(), expectedText);
            Assert.AreEqual(expectedText, File.ReadAllText(path));
        }
        FileAssert.DoesNotExist(path.ExpandEnv());
        DirectoryAssert.DoesNotExist(folder.ExpandEnv());
    }
}

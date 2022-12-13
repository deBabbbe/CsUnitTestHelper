
using ClassExtensions;
using TestHelper;

namespace TestHelperTest;

public class TempCreateFileInFolderTests
{
    [Test]
    public void TempCreateFileInFolderTests_CreatesAndDeletesFileInFolder()
    {
        const string folder = "%test%/Test/Dings";
        const string path = $"{folder}/Temp.txt";
        FileAssert.DoesNotExist(path.ExpandEnv());
        var expectedText = Guid.NewGuid().ToString();

        using (new TempSetEnvVar("%test%", "."))
        using (new TempCreateFileInFolder(path, expectedText))
        {
            FileAssert.Exists(path.ExpandEnv(), expectedText);
            Assert.AreEqual(expectedText, File.ReadAllText(path));
        }
        FileAssert.DoesNotExist(path.ExpandEnv());
        DirectoryAssert.DoesNotExist(folder.ExpandEnv());
    }

    [Test]
    public void TempCreateFileInFolderTests_PassedPathIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new TempCreateFileInFolder(null!, "irrelevant"));
    }

    [Test]
    public void TempCreateFileInFolderTests_PassedPathCannotBeExtracted()
    {
        Assert.Throws<ArgumentException>(() => new TempCreateFileInFolder(Guid.NewGuid().ToString(), "irrelevant"));
    }
}

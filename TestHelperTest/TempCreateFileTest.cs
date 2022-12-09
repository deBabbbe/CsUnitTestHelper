
using ClassExtensions;
using TestHelper;

namespace TestHelperTest;

public class TempCreateFileTests
{
    [Test]
    public void TempCreateFileTests_CreatesAndDeletesFile()
    {
        var path = "%temp%/Temp.txt";
        FileAssert.DoesNotExist(path.ExpandEnv());
        var expectedText = Guid.NewGuid().ToString();
        using (new TempCreateFile(path, expectedText))
        {
            FileAssert.Exists(path.ExpandEnv(), expectedText);
            Assert.AreEqual(expectedText, File.ReadAllText(path));
        }
        FileAssert.DoesNotExist(path.ExpandEnv());
    }
}


using ClassExtensions;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using TestHelper;

namespace TestHelperTest;

[TestFixture]
public class TempCreateFileTests
{
    [Test]
    public void TempCreateFileTests_CreatesAndDeletesFile()
    {
        const string path = "%temp%/Temp.txt";
        FileAssert.DoesNotExist(path.ExpandEnv());
        var expectedText = Guid.NewGuid().ToString();
        using (new TempSetEnvVar("temp", "."))
        using (new TempCreateFile(path, expectedText))
        {
            FileAssert.Exists(path.ExpandEnv(), expectedText);
            Assert.That(File.ReadAllText(path.ExpandEnv()), Is.EqualTo(expectedText));
        }
        FileAssert.DoesNotExist(path.ExpandEnv());
    }
}

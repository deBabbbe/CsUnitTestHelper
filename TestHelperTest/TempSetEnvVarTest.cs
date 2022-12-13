
using ClassExtensions;
using TestHelper;

namespace TestHelperTest;

public class TempSetEnvVarTest
{
    [Test]
    public void TempSetEnvVarTest_SetsEnvVar()
    {
        const string name = "Schorsch";
        var value = Guid.NewGuid().ToString();

        Assert.AreEqual($"%{name}%".ExpandEnv(), $"%{name}%");
        using (new TempSetEnvVar(name, value))
        {
            Assert.AreEqual($"%{name}%".ExpandEnv(), value);
        }
        Assert.AreEqual($"%{name}%".ExpandEnv(), $"%{name}%");
    }
}

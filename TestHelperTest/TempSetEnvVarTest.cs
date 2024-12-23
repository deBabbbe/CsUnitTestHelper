
using ClassExtensions;
using TestHelper;

namespace TestHelperTest;

[TestFixture]
public class TempSetEnvVarTest
{
    [Test]
    public void TempSetEnvVarTest_SetsEnvVar()
    {
        const string name = "Schorsch";
        var value = Guid.NewGuid().ToString();

        Assert.That($"%{name}%".ExpandEnv(), Is.EqualTo($"%{name}%"));
        using (new TempSetEnvVar(name, value))
        {
            Assert.That($"%{name}%".ExpandEnv(), Is.EqualTo(value));
        }
        Assert.That($"%{name}%".ExpandEnv(), Is.EqualTo($"%{name}%"));
    }

    [Test]
    public void TempSetEnvVarTest_ResetsExisting()
    {
        const string name = "Schorsch";
        var valueBefore = Guid.NewGuid().ToString();
        var value = Guid.NewGuid().ToString();

        Assert.That($"%{name}%".ExpandEnv(), Is.EqualTo($"%{name}%"));
        using (new TempSetEnvVar(name, valueBefore))
        {
            Assert.That($"%{name}%".ExpandEnv(), Is.EqualTo(valueBefore));

            using (new TempSetEnvVar(name, value))
            {
                Assert.That($"%{name}%".ExpandEnv(), Is.EqualTo(value));
            }

            Assert.That($"%{name}%".ExpandEnv(), Is.EqualTo(valueBefore));
        }
        Assert.That($"%{name}%".ExpandEnv(), Is.EqualTo($"%{name}%"));
    }
}

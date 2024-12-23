using System.Configuration;
using TestHelper;
using NUnit.Framework;

namespace TestHelperTest;

[TestFixture]
public class TempSetAppSettingTest
{
    [Test]
    public void GenerateRandomIntTest_ValueDoesNotExist()
    {
        const string key = "TestEntry";
        const string value = "TestValue";
        Assert.That(ConfigurationManager.AppSettings[key], Is.Null, $"{key} was already set");

        using (new TempSetAppSetting(key, value))
        {
            Assert.That(ConfigurationManager.AppSettings.AllKeys.Contains(key), Is.True, $"{key} was not set");
        }

        Assert.That(ConfigurationManager.AppSettings[key], Is.Null, $"{key} was not set to null");
    }

    [Test]
    public void GenerateRandomIntTest_ValueIsOverridden()
    {
        const string key = "TestEntry";
        const string initValue = "TestValue";
        const string value = "TestValue";
        ConfigurationManager.AppSettings[key] = initValue;

        Assert.That(ConfigurationManager.AppSettings[key], Is.EqualTo(initValue));

        using (new TempSetAppSetting(key, value))
        {
            Assert.That(ConfigurationManager.AppSettings[key], Is.EqualTo(value));
        }

        Assert.That(ConfigurationManager.AppSettings[key], Is.EqualTo(initValue));
    }
}
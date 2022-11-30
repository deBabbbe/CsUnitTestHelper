using TestHelper;

namespace TestHelperTest;

public class HelperTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GenerateRandomIntTest()
    {
        Assert.IsTrue(Helper.GenerateRandomInt() > 0, "Int smaller 0");
        Assert.IsTrue(Helper.GenerateRandomInt() <= 9, "Int greater than 9");
        Assert.AreNotEqual(Helper.GenerateRandomInt(), Helper.GenerateRandomInt());
    }

    [Test]
    public void GenerateRandomStringTest_DefaultValueIsUsed()
    {
        var result = Helper.GenerateRandomString();
        StringAssert.IsMatch("[A-Z]{10}", result);
    }

    [Test]
    public void GenerateRandomStringTest_SingleChar()
    {
        var count = 1;
        var result = Helper.GenerateRandomString(count);
        StringAssert.IsMatch($"[A-Z]{{{count}}}", result);
    }

    [Test]
    public void GenerateRandomStringTest_HundertChars()
    {
        var count = 100;
        var result = Helper.GenerateRandomString(count);
        StringAssert.IsMatch($"[A-Z]{{{count}}}", result);
    }

    [Test]
    public void GenerateRandomStringTest_RandomCount()
    {
        var count = Helper.GenerateRandomInt();
        var result = Helper.GenerateRandomString(count);
        StringAssert.IsMatch($"[A-Z]{{{count}}}", result);
    }
}
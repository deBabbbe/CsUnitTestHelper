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
        var result = Helper.GenerateRandomInt();

        var toCompare = Helper.GenerateRandomInt();
        if (toCompare == result) toCompare = Helper.GenerateRandomInt();

        Assert.IsTrue(result > 0, "Int smaller 0");
        Assert.IsTrue(result <= 9, "Int greater than 9");
        Assert.AreNotEqual(toCompare, result);
    }

    [Test]
    public void GenerateRandomIntTest_WithRange()
    {
        var min = 100;
        var max = 100000;
        var results = Helper.GenerateRandomList(() => Helper.GenerateRandomInt(min, max), 100);
        results.ForEach(result =>
        {
            Assert.IsTrue(result >= min, $"Int smaller or equal than {min}");
            Assert.IsTrue(result <= max, $"Int greater or equal than {max}");
            Assert.AreNotEqual(Helper.GenerateRandomInt(), result);
        });
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

    [Test]
    public void GenerateRandomListTest_SingleEntry()
    {
        var count = Helper.GenerateRandomInt();
        var expected = new List<string> { "Test" };
        var result = Helper.GenerateRandomList(() => "Test", 1);
        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void GenerateRandomListTest_SingleEntry_DifferentType()
    {
        var count = Helper.GenerateRandomInt();
        var expected = new List<int> { 1 };
        var result = Helper.GenerateRandomList(() => 1, 1);
        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void GenerateRandomListTest_FiveEntries()
    {
        var count = Helper.GenerateRandomInt();
        var expected = new List<string> { "ABC", "ABC", "ABC", "ABC", "ABC" };
        var result = Helper.GenerateRandomList(() => "ABC", 5);
        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void GenerateRandomListTest_MultipleEntriesWithDifferentValues()
    {
        var count = 0;
        string GetAlphabet() => ((char)('A' + count++)).ToString();
        var expected = new List<string> { "A", "B", "C", "D", "E" };

        var result = Helper.GenerateRandomList(GetAlphabet, 5);

        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void GenerateRandomBoolTest()
    {
        var randomBools = Helper.GenerateRandomList(Helper.GenerateRandomBool, 100);
        Assert.AreEqual(2, randomBools.Distinct().Count());
    }

    [Test]
    public void GetGenerateRandomDateTimeTest()
    {
        var result = Helper.GetGenerateRandomDateTime();

        Assert.AreNotEqual(DateTime.MinValue, result);

        Assert.IsTrue(result.Year >= 1870);
        Assert.IsTrue(result.Year < 2300);

        Assert.IsTrue(result.Month >= 1);
        Assert.IsTrue(result.Month <= 12);

        Assert.IsTrue(result.Day >= 1);
        Assert.IsTrue(result.Day < 27);

        Assert.IsTrue(result.Hour >= 1);
        Assert.IsTrue(result.Hour < 24);

        Assert.IsTrue(result.Minute >= 0);
        Assert.IsTrue(result.Minute < 60);

        Assert.IsTrue(result.Second >= 0);
        Assert.IsTrue(result.Second < 60);
    }

    [Test]
    public void GenerateRandomStringGuidWithPrefixTest()
    {
        var prefix = Helper.GenerateRandomString();
        var result = Helper.GenerateRandomStringGuidWithPrefix(prefix);
        StringAssert.StartsWith(prefix, result);
        var isParsable = Guid.TryParse(result.Replace(prefix, ""), out var _);
        Assert.IsTrue(isParsable, "Guid not parsable");
    }
}
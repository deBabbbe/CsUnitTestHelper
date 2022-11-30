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
}
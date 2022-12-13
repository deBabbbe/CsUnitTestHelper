using System.Collections.Immutable;
using System.Security.Cryptography;
using ClassExtensions;
using TestHelper;

namespace TestHelperTest;

public class HelperTests
{
    [Test]
    public void GenerateRandomIntTest()
    {
        var result = Helper.GenerateRandomInt();

        var toCompare = Helper.GenerateRandomInt();
        if (toCompare == result) toCompare = Helper.GenerateRandomInt();

        Assert.IsTrue(result.IsInBetween(1, 10));
        Assert.AreNotEqual(toCompare, result);
    }

    [Test]
    public void GenerateRandomIntTest_WithRange()
    {
        const int min = 100;
        const int max = 100000;
        var results = Helper.GenerateRandomList(() => Helper.GenerateRandomInt(min, max), 100);
        results.ForEach(result =>
        {
            Assert.IsTrue(result.IsInBetween(min, max));
            Assert.AreNotEqual(Helper.GenerateRandomInt(), result);
        });
    }

    [Test]
    public void GenerateRandomIntTest_DefaultRange()
    {
        const int min = 100;
        const int max = 100000;
        var results = Helper.GenerateRandomList(() => Helper.GenerateRandomInt(min, max));
        results.ForEach(result =>
        {
            Assert.IsTrue(result.IsInBetween(min, max));
            Assert.AreEqual(10, results.Count);
        });
    }

    [Test]
    public void GenerateRandomStringTest_DefaultValueIsUsed()
    {
        var result = Helper.GenerateRandomString();
        StringAssert.IsMatch("[A-z]{10}", result);
    }

    [Test]
    public void GenerateRandomStringTest_SingleChar()
    {
        const int count = 1;
        var result = Helper.GenerateRandomString(count);
        StringAssert.IsMatch($"[A-z]{{{count}}}", result);
    }

    [Test]
    public void GenerateRandomStringTest_HundertChars()
    {
        const int count = 100;
        var result = Helper.GenerateRandomString(count);
        StringAssert.IsMatch($"[A-z]{{{count}}}", result);
    }

    [Test]
    public void GenerateRandomStringTest_RandomCount()
    {
        var count = Helper.GenerateRandomInt(1000, 5000);
        var result = Helper.GenerateRandomString(count);
        StringAssert.IsMatch($"[A-z]{{{count}}}", result);
        StringAssert.IsMatch("[a-z]", result);
    }

    [Test]
    public void GenerateRandomListTest_SingleEntry()
    {
        var expected = new List<string> { "Test" };
        var result = Helper.GenerateRandomList(() => "Test", 1);
        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void GenerateRandomListTest_SingleEntry_DifferentType()
    {
        var expected = new List<int> { 1 };
        var result = Helper.GenerateRandomList(() => 1, 1);
        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public void GenerateRandomListTest_FiveEntries()
    {
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
    public void GenerateRandomDateTimeTest()
    {
        var result = Helper.GenerateRandomDateTime();

        Assert.AreNotEqual(DateTime.MinValue, result);
        Assert.IsTrue(result.Year.IsInBetween(1870, 2300));
        Assert.IsTrue(result.Month.IsInBetween(1, 12));
        Assert.IsTrue(result.Day.IsInBetween(1, 26));
        Assert.IsTrue(result.Hour.IsInBetween(0, 23));
        Assert.IsTrue(result.Minute.IsInBetween(0, 59));
        Assert.IsTrue(result.Second.IsInBetween(0, 59));
    }

    [Test]
    public void GenerateRandomStringGuidWithPrefixTest()
    {
        var prefix = Helper.GenerateRandomString();
        var result = Helper.GenerateRandomStringGuidWithPrefix(prefix);
        StringAssert.StartsWith(prefix, result);
        var isParsable = Guid.TryParse(result.Replace(prefix, ""), out var parsedGuid);
        Assert.IsTrue(isParsable, "Guid not parsable");
        Assert.AreNotEqual(Guid.Empty, parsedGuid);
    }

    [Test]
    public void ToRandomCaseTest()
    {
        var text = Helper.GenerateRandomString(100);
        Assert.AreNotEqual(text.ToRandomCase(), text.ToRandomCase());
    }

    [Test]
    public void AnyOneTest()
    {
        var list = Helper.GenerateRandomList(() => Guid.NewGuid().ToString());
        300.Times(_ =>
        {
            CollectionAssert.Contains(list, list.AnyOne());
        });
    }

    [Test]
    public void AnyOneTest_Except()
    {
        ImmutableList<string> list = Helper.GenerateRandomList(() => Guid.NewGuid().ToString(), 100).ToImmutableList();
        var idx = RandomNumberGenerator.GetInt32(0, list.Count());

        300.Times(_ =>
        {
            var except = list[idx];
            var entry = list.AnyOne(except);
            CollectionAssert.Contains(list, entry);
            Assert.AreNotEqual(except, entry);
        });
    }

    [Test]
    public void AnyOneTest_ExceptList()
    {
        var list = Helper.GenerateRandomList(() => Guid.NewGuid().ToString(), 100);
        var idx1 = RandomNumberGenerator.GetInt32(0, list.Count());
        var idx2 = RandomNumberGenerator.GetInt32(0, list.Count());

        300.Times(_ =>
        {
            var entry = list.AnyOne(new[] { list[idx1], list[idx2] });
            CollectionAssert.Contains(list, entry);
            Assert.AreNotEqual(list[idx1], entry);
            Assert.AreNotEqual(list[idx2], entry);
        });
    }

    [Test]
    public void PopTest()
    {
        var expected = new List<string> { "a", "b", "c", "d" };
        var list = new List<string> { "a", "b", "c", "d" };
        list.Count.Times(t =>
        {
            var result = list.Pop();
            Assert.AreEqual(expected[expected.Count - t - 1], result);
            Assert.AreEqual(expected.Count - (t + 1), list.Count);
        });
    }

    [Test]
    public void PopTest_DifferentList()
    {
        var expected = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
        list.Count.Times(t =>
        {
            var result = list.Pop();
            Assert.AreEqual(expected[expected.Count - t - 1], result);
            Assert.AreEqual(expected.Count - (t + 1), list.Count);
        });
    }

    [Test]
    public void PopTest_OnEmptyList()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new List<string>().Pop());
    }
}
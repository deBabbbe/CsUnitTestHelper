using System.Diagnostics;
using TestHelper;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TestHelperTest;

[TestFixture]
public class HelperTests
{
    [Test]
    public void GenerateRandomIntTest()
    {
        var result = Helper.GenerateRandomInt();

        var toCompare = Helper.GenerateRandomInt();
        if (toCompare == result) toCompare = Helper.GenerateRandomInt();

        Assert.That(result.IsInBetween(1, 10), Is.True);
        CollectionAssert.AreNotEqual(new[] { toCompare }, new[] { result });
    }

    [Test]
    public void GenerateRandomIntTest_WithRange()
    {
        const int min = 100;
        const int max = 100000;
        var results = Helper.GenerateRandomList(() => Helper.GenerateRandomInt(min, max), 100);
        results.ForEach(result =>
        {
            Assert.That(result.IsInBetween(min, max), Is.True);
            CollectionAssert.AreNotEqual(new[] { Helper.GenerateRandomInt() }, new[] { result });
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
        Assert.That(randomBools.Distinct().Count(), Is.EqualTo(2));
    }

    [Test]
    public void GenerateRandomDateTimeTest()
    {
        var result = Helper.GenerateRandomDateTime();

        Assert.That(result, Is.Not.EqualTo(DateTime.MinValue));
        Assert.That(result.Year.IsInBetween(1870, 2300), Is.True);
        Assert.That(result.Month.IsInBetween(1, 12), Is.True);
        Assert.That(result.Day.IsInBetween(1, 26), Is.True);
        Assert.That(result.Hour.IsInBetween(0, 23), Is.True);
        Assert.That(result.Minute.IsInBetween(0, 59), Is.True);
        Assert.That(result.Second.IsInBetween(0, 59), Is.True);
    }

    [Test]
    public void GenerateRandomStringGuidWithPrefixTest()
    {
        var prefix = Helper.GenerateRandomString();
        var result = Helper.GenerateRandomStringGuidWithPrefix(prefix);
        StringAssert.StartsWith(prefix, result);
        var isParsable = Guid.TryParse(result.Replace(prefix, ""), out var parsedGuid);
        Assert.That(isParsable, Is.True, "Guid not parsable");
        CollectionAssert.AreNotEqual(new[] { Guid.Empty }, new[] { parsedGuid });
    }

    [Test]
    public void ToRandomCaseTest()
    {
        var text = Helper.GenerateRandomString(100);
        Assert.That(text.ToRandomCase(), Is.Not.EqualTo(text.ToRandomCase()));
    }

    [Test]
    public void ToRandomCaseTest_CheckByRegex()
    {
        StringAssert.IsMatch("[aA][bB][cC]", "ABC".ToRandomCase());
    }

    [TestCase(5, 1, 10, ExpectedResult = true)]
    [TestCase(4, 0, 10, ExpectedResult = true)]
    [TestCase(3, -10, 10, ExpectedResult = true)]
    [TestCase(2, 1, 3, ExpectedResult = true)]
    [TestCase(1, 1, 1, ExpectedResult = true)]
    [TestCase(0, 1, 1, ExpectedResult = false)]
    [TestCase(-1, 0, 1, ExpectedResult = false)]
    [TestCase(7, 10, 5, ExpectedResult = false)]
    public bool IsInBetweenTest(int value, int min, int max)
    {
        return value.IsInBetween(min, max);
    }

    [TestCase(typeof(IgnoreAttribute), ExpectedResult = true)]
    [TestCase(typeof(DescriptionAttribute), ExpectedResult = true)]
    [TestCase(typeof(TestAttribute), ExpectedResult = false)]
    [TestCase(typeof(TestActionAttribute), ExpectedResult = false)]
    [TestCase(typeof(ObsoleteAttribute), ExpectedResult = false)]
    [TestCase(typeof(ConditionalAttribute), ExpectedResult = false)]
    public bool HasAttributeTest_Class(Type type)
    {
        return new ClassWithAttribute().HasAttribute(type);
    }

    [TestCase(typeof(ObsoleteAttribute), ExpectedResult = true)]
    [TestCase(typeof(MyAttribute), ExpectedResult = true)]
    [TestCase(typeof(IgnoreAttribute), ExpectedResult = false)]
    [TestCase(typeof(DescriptionAttribute), ExpectedResult = false)]
    [TestCase(typeof(TestAttribute), ExpectedResult = false)]
    [TestCase(typeof(TestActionAttribute), ExpectedResult = false)]
    [TestCase(typeof(ConditionalAttribute), ExpectedResult = false)]
    [Obsolete]
    public bool HasPropertyWithAttributeTest_Property(Type type)
    {
        return new ClassWithAttribute()
            .HasPropertyWithAttribute(nameof(ClassWithAttribute.MyProperty), type);
    }

    [Test(ExpectedResult = false)]
    public bool HasAttributeTest_Property_DoesNotExist()
    {
        return new ClassWithAttribute()
            .HasPropertyWithAttribute("NonExisting", typeof(ObsoleteAttribute));
    }

    [Ignore("Ignore text")]
    [Description("I dont know")]
    private class ClassWithAttribute
    {
        [Obsolete]
        [MyAttribute]
        public int MyProperty { get; set; }
    }

    private class MyAttribute : Attribute { }
}
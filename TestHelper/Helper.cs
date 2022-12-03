namespace TestHelper;

public static class Helper
{
    private static Random _random = new Random();

    public static int GenerateRandomInt(int min = 1, int max = 10) =>
        _random.Next(min, max + 1);

    public static string GenerateRandomString(int numberOfCharacters = 10) =>
        string.Join("", Enumerable.Range(0, numberOfCharacters)
            .Select(_ => (char)_random.Next('A', 'z')));

    public static List<T> GenerateRandomList<T>(Func<T> Generator, int numberOfElements)
    {
        return Enumerable.Range(0, numberOfElements)
            .Select(_ => Generator())
            .ToList();
    }

    public static bool GenerateRandomBool() => _random.Next(0, 2) == 1;

    public static DateTime GetGenerateRandomDateTime()
    {
        var year = _random.Next(1870, 2301);
        var month = _random.Next(1, 13);
        var day = _random.Next(1, 27);
        var hour = _random.Next(0, 24);
        var minute = _random.Next(0, 60);
        var second = _random.Next(0, 60);

        return new DateTime(year, month, day, hour, minute, second);
    }

    public static string GenerateRandomStringGuidWithPrefix(string prefix) =>
        $"{prefix}{Guid.NewGuid().ToString()}";

    public static string ToRandomCase(this string text)
    {
        string RandomCase(char charText) => _random.Next(0, 2) == 1 ?
            charText.ToString().ToUpper() :
            charText.ToString().ToLower();

        return string.Join(",", text.Select(RandomCase));
    }
}

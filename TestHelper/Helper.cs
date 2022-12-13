using System.Security.Cryptography;

namespace TestHelper;

public static class Helper
{
    public static int GenerateRandomInt(int min = 1, int max = 10) =>
        RandomNumberGenerator.GetInt32(min, max + 1);

    public static string GenerateRandomString(int numberOfCharacters = 10) =>
        string.Join("", Enumerable.Range(0, numberOfCharacters)
            .Select(_ => (char)RandomNumberGenerator.GetInt32('A', 'z')));

    public static List<T> GenerateRandomList<T>(Func<T> Generator, int numberOfElements = 10) =>
        Enumerable.Range(0, numberOfElements)
            .Select(_ => Generator())
            .ToList();

    public static bool GenerateRandomBool() => RandomNumberGenerator.GetInt32(2) == 0;

    public static DateTime GenerateRandomDateTime()
    {
        var year = RandomNumberGenerator.GetInt32(1870, 2301);
        var month = RandomNumberGenerator.GetInt32(1, 13);
        var day = RandomNumberGenerator.GetInt32(1, 27);
        var hour = RandomNumberGenerator.GetInt32(0, 24);
        var minute = RandomNumberGenerator.GetInt32(0, 60);
        var second = RandomNumberGenerator.GetInt32(0, 60);

        return new DateTime(year, month, day, hour, minute, second);
    }

    public static string GenerateRandomStringGuidWithPrefix(string prefix) =>
        $"{prefix}{Guid.NewGuid().ToString()}";

    public static string ToRandomCase(this string text)
    {
        return string.Join(",", text.Select(ConvertCharToRandomCase));
    }

    public static T AnyOne<T>(this IEnumerable<T> src) where T : class =>
        src.AnyOne<T>((T)null);

    public static T AnyOne<T>(this IEnumerable<T> src, T except) where T : class =>
        src.AnyOne(new[] { except });

    public static T AnyOne<T>(this IEnumerable<T> src, IEnumerable<T> except) where T : class
    {
        var idx = RandomNumberGenerator.GetInt32(0, src.Count() - except.Count() - 1);
        return src.Where(s => !except.Contains(s)).ElementAt(idx);
    }

    public static T Pop<T>(this IList<T> src)
    {
        var idx = src.Count() - 1;
        var result = src.ElementAt(idx);
        src.RemoveAt(idx);
        return result;
    }

    public static T Shift<T>(this IList<T> src)
    {
        var result = src.ElementAt(0);
        src.RemoveAt(0);
        return result;
    }

    public static void Unshift<T>(this IList<T> src, T toAdd) => src.Insert(0, toAdd);

    private static string ConvertCharToRandomCase(char charText) =>
        RandomNumberGenerator.GetInt32(0, 2) == 1 ?
            charText.ToString().ToUpper() :
            charText.ToString().ToLower();
}

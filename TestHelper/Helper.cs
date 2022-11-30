using System.Text;

namespace TestHelper;

public static class Helper
{
    private static Random _random = new Random();
    public static int GenerateRandomInt()
    {
        return _random.Next(1, 10);
    }

    public static string GenerateRandomString(int numberOfCharacters = 10)
    {
        var builder = new StringBuilder(numberOfCharacters);
        var offset = 'A';
        const int lettersOffset = 26;

        for (var i = 0; i < numberOfCharacters; i++)
        {
            var @char = (char)_random.Next(offset, offset + lettersOffset);
            builder.Append(@char);
        }

        return builder.ToString();
    }

    public static List<T> GenerateRandomList<T>(Func<T> Generator, int numberOfElements)
    {
        return Enumerable.Range(0, numberOfElements).Select(_ => Generator()).ToList();
    }

    public static bool GenerateRandomBool()
    {
        return _random.Next(0, 2) == 1;
    }
}

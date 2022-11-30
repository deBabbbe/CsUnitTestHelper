using System.Text;

namespace TestHelper;

public static class Helper
{
    private static Random _random = new Random();
    public static int GenerateRandomInt()
    {
        return _random.Next(1, 10);
    }

    public static string GenerateRandomString(int size = 10)
    {
        var builder = new StringBuilder(size);
        var offset = 'A';
        const int lettersOffset = 26;

        for (var i = 0; i < size; i++)
        {
            var @char = (char)_random.Next(offset, offset + lettersOffset);
            builder.Append(@char);
        }

        return builder.ToString();
    }
}

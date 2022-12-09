﻿using System.Security.Cryptography;

namespace TestHelper;

public static class Helper
{
    private static Random _random = new Random();

    public static int GenerateRandomInt(int min = 1, int max = 10) =>
        RandomNumberGenerator.GetInt32(min, max + 1);

    public static string GenerateRandomString(int numberOfCharacters = 10) =>
        string.Join("", Enumerable.Range(0, numberOfCharacters)
            .Select(_ => (char)_random.Next('A', 'z')));

    public static List<T> GenerateRandomList<T>(Func<T> Generator, int numberOfElements) =>
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
        string RandomCase(char charText) => RandomNumberGenerator.GetInt32(0, 2) == 1 ?
            charText.ToString().ToUpper() :
            charText.ToString().ToLower();

        return string.Join(",", text.Select(RandomCase));
    }
}

# 🧪 CsUnitTestHelper for C#

[![.NET Core Desktop](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/dotnet-desktop.yml)
[![CodeQL](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/codeql.yml/badge.svg)](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/codeql.yml)
[![Codacy Security Scan](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/codacy.yml/badge.svg)](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/codacy.yml)
[![SecurityCodeScan](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/securitycodescan.yml/badge.svg)](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/securitycodescan.yml)

A **unit test helper library** for C# providing random data generators, temporary file/directory helpers, and handy extension methods.

---

## 🎲 Random Generators

- `int GenerateRandomInt()` – Returns a random integer between **1 and 9**  
- `int GenerateRandomInt(int min, int max)` – Returns a random integer between `min` and `max`  
- `string GenerateRandomString()` – Returns a random string of **10 characters (A-z)**  
- `string GenerateRandomString(int numberOfCharacters)` – Returns a random string with specified length  
- `List<T> GenerateRandomList<T>(Func<T> generator, int numberOfElements)` – Creates a list of random elements using a generator function  
- `bool GenerateRandomBool()` – Returns a random boolean  
- `DateTime GetGenerateRandomDateTime()` – Returns a random `DateTime` between **1870 and 2300**  
- `string GenerateRandomStringGuidWithPrefix(string prefix)` – Returns a GUID as string with the given prefix  

---

## ✨ String Utilities

- `string ToRandomCase(this string text)` – Returns the string in **random case**

---

## 📁 Temporary File & Directory Helpers

- `TempCreateDirectory(string path)` – Creates a directory, deletes it when disposed  
- `TempCreateFile(string path, string content)` – Creates a file, deletes it when disposed  
- `TempCreateFileInFolder(string path, string content)` – Creates a file in a folder, deletes both when disposed  
- `TempSetEnvVar(string var, string value)` – Sets an environment variable temporarily, restores original value when disposed  
- `TempSetAppSetting(string key, string value)` – Sets an app setting temporarily, restores original value when disposed  
- `TempDeleteFile(string filePath)` – Removes a file temporarily and restores it when disposed  

---

## 🔎 Extension & Helper Methods

- `static bool IsInBetween(this int value, int min, int max)` – Checks if value is between `min` and `max`, logs message if not  
- `static bool HasAttribute<T>(this T _, Type attribute) where T : class` – Returns `true` if a class has the attribute  
- `static bool HasPropertyWithAttribute<T>(this T _, string propertyName, Type attribute)` – Returns `true` if a property exists and has the attribute
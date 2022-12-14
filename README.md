[![.NET Core Desktop](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/dotnet-desktop.yml)
[![CodeQL](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/codeql.yml/badge.svg)](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/codeql.yml)
[![Codacy Security Scan](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/codacy.yml/badge.svg)](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/codacy.yml)
[![SecurityCodeScan](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/securitycodescan.yml/badge.svg)](https://github.com/deBabbbe/CsUnitTestHelper/actions/workflows/securitycodescan.yml)

# Unit test helper for C#

### int GenerateRandomInt()

Creates a random integer between 1 and 9

### int GenerateRandomInt(int min, int max)

Creates a random integer between min and max

### string GenerateRandomString()

Creates a random string with chars from A-z with 10 chars

### string GenerateRandomString(int numberOfCharacters)

Creates a random string with chars from A-z with passed numberOfCharacters

### List\<T\> GenerateRandomList\<T\>(Func\<T\> Generator, int numberOfElements)

Creates a random list of given type with elements from the generator

### bool GenerateRandomBool()

Creates a random bool

### DateTime GetGenerateRandomDateTime()

Creates a random DateTime between 1870 and 2300

### string GenerateRandomStringGuidWithPrefix(string prefix)

Returns a GUID as string with the given prefix

### string ToRandomCase(this string text)

Returns the passed string in random case

### TempCreateDirectory(string path)

Creates a directory and deletes it, when disposed

### TempCreateFile(string path, string content)

Creates a file and deletes it, when disposed

### TempCreateFileInFolder(string path, string content)

Creates a file in a folder and deletes both, when disposed

### TempSetEnvVar(string var, string value)

Sets a env variable to value and sets it back to the value before, when disposed

### static bool IsInBetween(this int value, int min, int max)

Checks if value is in between of min and max and logs message to console, if not

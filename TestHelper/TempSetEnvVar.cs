using ClassExtensions;

namespace TestHelper;

public class TempSetEnvVar: IDisposable
{
    private readonly string _envVarName;

    public TempSetEnvVar(string var, string value)
    {
        _envVarName = var;
        Environment.SetEnvironmentVariable(var, value);
    }

    public void Dispose()
    {
        Environment.SetEnvironmentVariable(_envVarName, null);
    }
}

using ClassExtensions;

namespace TestHelper;

public class TempSetEnvVar: IDisposable
{
    private string _var;

    public TempSetEnvVar(string var, string value)
    {
        _var = var;
        Environment.SetEnvironmentVariable(var, value);
    }

    public void Dispose()
    {
        Environment.SetEnvironmentVariable(_var, null);
    }
}
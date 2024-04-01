using UnityEngine;

public static class EnvironmentVariables
{
    public static string GetEnv(string name)
    {
        return System.Environment.GetEnvironmentVariable(name);
    }
}

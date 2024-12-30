namespace SvcCom.Exceptions;

public class AssemblyLoadException : Exception
{
    public string AssemblyPath { get; }

    public AssemblyLoadException(string assemblyPath, Exception? innerException = null)
        : base($"Unable to load assembly: '{assemblyPath}'.", innerException)
    {
        AssemblyPath = assemblyPath;
    }
}
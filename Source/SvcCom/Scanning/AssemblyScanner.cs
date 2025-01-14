using System.Reflection;
using SvcCom.Exceptions;

namespace SvcCom.Scanning;

public class AssemblyScanner
{
    private Assembly Assembly { get; }
    
    public AssemblyScanner(string assemblyPath)
    {
        try
        {
            Assembly = Assembly.LoadFrom(assemblyPath);
        }
        catch (Exception ex)
        {
            throw new AssemblyLoadException(assemblyPath, ex);
        }
    }
}
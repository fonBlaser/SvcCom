namespace SvcCom.Exceptions;

public class AssemblyLoadException : Exception
{
    public string Name { get; }
    public string Path { get; }

    public AssemblyLoadException(string path, Exception? inner = null)
        : base($"Unable to load assembly from path '{path}'", inner)
    {
        Path = path;
        Name = System.IO.Path.GetFileName(path);
    }
}
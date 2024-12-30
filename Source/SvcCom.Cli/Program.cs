using System.Reflection;

string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0.0";

Console.WriteLine($"SComm CLI v{assemblyVersion}");

Console.WriteLine("Loaded assemblies:");
foreach (AssemblyName assemblyName in AppDomain.CurrentDomain.GetAssemblies().Select(a => a.GetName()))
{
    Console.WriteLine($"- {assemblyName.Name} v{assemblyName.Version}");
}

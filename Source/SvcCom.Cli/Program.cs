using System.Reflection;

string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0.0";

Console.WriteLine($"SvcCom CLI (SComm) v{assemblyVersion}");

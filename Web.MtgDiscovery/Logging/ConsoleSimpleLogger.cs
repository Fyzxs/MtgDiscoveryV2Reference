using System;
using Lib.UniversalCore.Logging;

namespace Web.MtgDiscovery.Logging;

public sealed class ConsoleSimpleLogger : ISimpleLogger
{
    public void LogVerbose(string message)
    {
        Console.WriteLine($"{DateTime.Now:s} {"VERBOSE",-7} {message}");
    }

    public void LogDebug(string message)
    {
        Console.WriteLine($"{DateTime.Now:s} {"DEBUG",-7} {message}");
    }

    public void LogInformation(string message)
    {
        Console.WriteLine($"{DateTime.Now:s} {"INFO",-7} {message}");
    }

    public void LogDuringQuiet(string message)
    {
        Console.WriteLine($"{DateTime.Now:s} {"INFO",-7} {message}");
    }

    public void LogException(string message, Exception ex)
    {
        Console.WriteLine($"{DateTime.Now:s} {"EXCPTN",-7} {message} {Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}");
    }
}

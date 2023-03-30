using System;

namespace Lib.UniversalCore.Logging;

public interface ISimpleLogger
{
    void LogInformation(string message);
    void LogException(string message, Exception ex);
    void LogDebug(string message);
    void LogVerbose(string message);
    void LogDuringQuiet(string message);
}

public sealed class SimplisticConsoleSimpleLogger : ISimpleLogger
{
    public void LogVerbose(string message) { }

    public void LogDuringQuiet(string message) => Console.WriteLine($"{DateTime.Now:s} {"REQD",-7} {message}");

    public void LogDebug(string message) => Console.WriteLine($"{DateTime.Now:s} {"DEBUG",-7} {message}");

    public void LogInformation(string message) => Console.WriteLine($"{DateTime.Now:s} {"INFO",-7} {message}");

    public void LogException(string message, Exception ex) => Console.WriteLine($"{DateTime.Now:s} {"EXCPTN",-7} {message} {Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}");
}

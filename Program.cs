using Avalonia;
using System;
using System.Runtime.InteropServices;

namespace StereoPotato;

sealed class Program
{
    // Add a console window to the application so Console.WriteLine outputs are visible
    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        AllocConsole();
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}

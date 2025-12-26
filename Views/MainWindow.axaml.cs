using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.IO;
using StereoPotato.Utils;

namespace StereoPotato.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void OpenFile_Click(object? sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "Open WAV file",
            AllowMultiple = false,
            Filters =
            {
                new FileDialogFilter
                {
                    Name = "Select a WAV",
                    Extensions = { "wav" }
                }
            }
        };

        var result = await openFileDialog.ShowAsync(this);

        if (result == null || result.Length == 0)
            return;

        string path = result[0];

        Console.WriteLine("=== FILE INFO ===");
        Console.WriteLine($"Path : {path}");

        var fileInfo = new FileInfo(path);
        Console.WriteLine($"Size : {fileInfo.Length / 1024.0:F2} KB");

        Console.WriteLine("\n=== WAV INFO ===");
        WavInfoReader.PrintWavInfo(path);
    }

    private void Exit_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}

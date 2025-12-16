using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

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
                    Name = "WAV audio",
                    Extensions = { "wav" }
                }
            }
        };

        var result = await openFileDialog.ShowAsync(this);

        if (result != null && result.Length > 0)
        {
            Console.WriteLine($"Fichier sélectionné : {result[0]}");
        }
    }

    private void Exit_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}
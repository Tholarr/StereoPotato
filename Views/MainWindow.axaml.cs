using Avalonia.Controls;
using Avalonia.Interactivity;
using StereoPotato.Controls;
using StereoPotato.Readers;
using System;
using System.IO;
using NAudio.Wave;

namespace StereoPotato.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void OpenFile_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Select WAV file",
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

        var result = await dialog.ShowAsync(this);
        if (result == null || result.Length == 0)
            return;

        string path = result[0];

        Console.WriteLine("=== FILE INFO ===");
        Console.WriteLine($"Path : {path}");
        Console.WriteLine($"Size : {new FileInfo(path).Length / 1024.0:F2} KB");

        using (var reader = new WaveFileReader(path))
        {
            Console.WriteLine("\n=== WAV INFO ===");
            Console.WriteLine($"Channels     : {reader.WaveFormat.Channels}");
            Console.WriteLine($"Sample Rate  : {reader.WaveFormat.SampleRate} Hz");
            Console.WriteLine($"Bits/Sample  : {reader.WaveFormat.BitsPerSample}");
            Console.WriteLine($"Duration     : {reader.TotalTime.TotalSeconds:F2} seconds");
        }

        // float[] samples = WavSampleReader.ReadMonoSamples(path);
        var samples = WavSampleReader.ReadStereoSamples(path);
        Waveform.Samples = samples;

        Console.WriteLine("Waveform updated!");
    }

    private void Exit_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}

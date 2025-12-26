using System;
using System.IO;

namespace StereoPotato.Utils;

public static class WavInfoReader
{
    public static void PrintWavInfo(string path)
    {
        using var fs = File.OpenRead(path);
        using var reader = new BinaryReader(fs);

        // RIFF header
        string riff = new string(reader.ReadChars(4));
        reader.ReadInt32();
        string wave = new string(reader.ReadChars(4));

        if (riff != "RIFF" || wave != "WAVE")
        {
            Console.WriteLine("Invalid WAV file");
            return;
        }

        short channels = 0;
        int sampleRate = 0;
        short bitsPerSample = 0;
        int dataSize = 0;

        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            string chunkId = new string(reader.ReadChars(4));
            int chunkSize = reader.ReadInt32();

            if (chunkId == "fmt ")
            {
                reader.ReadInt16(); // audio format
                channels = reader.ReadInt16();
                sampleRate = reader.ReadInt32();
                reader.ReadInt32(); // byte rate
                reader.ReadInt16(); // block align
                bitsPerSample = reader.ReadInt16();

                reader.BaseStream.Position += chunkSize - 16;
            }
            else if (chunkId == "data")
            {
                dataSize = chunkSize;
                break;
            }
            else
            {
                reader.BaseStream.Position += chunkSize;
            }
        }

        double duration =
            dataSize / (sampleRate * channels * (bitsPerSample / 8.0));

        Console.WriteLine($"Channels     : {channels}");
        Console.WriteLine($"Sample Rate  : {sampleRate} Hz");
        Console.WriteLine($"Bits/Sample  : {bitsPerSample}");
        Console.WriteLine($"Duration     : {duration:F2} seconds");
    }
}

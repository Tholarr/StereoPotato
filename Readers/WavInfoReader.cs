using System;
using System.IO;
using StereoPotato.Models;

namespace StereoPotato.Readers
{
    public static class WavInfoReader
    {
        public static WavInfo Read(string path)
        {
            var fileInfo = new FileInfo(path);

            short channels = 0;
            int sampleRate = 0;
            short bitsPerSample = 0;
            int dataSize = 0;

            using var reader = new BinaryReader(File.OpenRead(path));

            var riff = new string(reader.ReadChars(4));
            reader.ReadInt32(); // file size
            var wave = new string(reader.ReadChars(4));

            if (riff != "RIFF" || wave != "WAVE")
                throw new InvalidDataException("Not a valid WAV file");

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                var chunkId = new string(reader.ReadChars(4));
                var chunkSize = reader.ReadInt32();

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
                dataSize / (double)(sampleRate * channels * (bitsPerSample / 8.0));

            var info = new WavInfo
            {
                Path = path,
                FileSizeBytes = fileInfo.Length,
                Channels = channels,
                SampleRate = sampleRate,
                BitsPerSample = bitsPerSample,
                DurationSeconds = duration
            };

            Print(info);

            return info;
        }

        private static void Print(WavInfo info)
        {
            Console.WriteLine("\n=== FILE INFO ===");
            Console.WriteLine($"Path : {info.Path}");
            Console.WriteLine($"Size : {info.FileSizeBytes / 1024.0:F2} KB");

            Console.WriteLine("\n=== WAV INFO ===");
            Console.WriteLine($"Channels     : {info.Channels}");
            Console.WriteLine($"Sample Rate  : {info.SampleRate} Hz");
            Console.WriteLine($"Bits/Sample  : {info.BitsPerSample}");
            Console.WriteLine($"Duration     : {info.DurationSeconds:F2} seconds");
        }
    }
}

using System;
using System.Collections.Generic;
using NAudio.Wave;
using StereoPotato.Models;

namespace StereoPotato.Readers
{
    public static class WavSampleReader
    {
        public static AudioSamples ReadStereoSamples(string path)
        {
            Console.WriteLine(">>> ReadStereoSamples CALLED <<<");

            using var reader = new AudioFileReader(path);

            if (reader.WaveFormat.Channels != 2)
                throw new InvalidOperationException("File is not stereo");

            var left = new List<float>();
            var right = new List<float>();

            float[] buffer = new float[1024 * reader.WaveFormat.Channels];
            int samplesRead;
            int frameIndex = 0;

            while ((samplesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
            {
                for (int i = 0; i < samplesRead; i += 2)
                {
                    float l = buffer[i];
                    float r = buffer[i + 1];

                    // DEBUG: first frames only
                    if (frameIndex < 8)
                    {
                        Console.WriteLine(
                            $"[AUDIO] {frameIndex} | L={l:F6} R={r:F6}"
                        );
                    }

                    left.Add(l);
                    right.Add(r);
                    frameIndex++;
                }
            }

            // Final verification
            Console.WriteLine("=== STORED SAMPLES CHECK ===");
            for (int i = 0; i < Math.Min(8, left.Count); i++)
            {
                Console.WriteLine(
                    $"[BUFFER] {i} | L={left[i]:F6} R={right[i]:F6}"
                );
            }

            return new AudioSamples(left.ToArray(), right.ToArray());
        }
    }
}

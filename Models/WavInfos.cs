namespace StereoPotato.Models
{
    public struct WavInfo
    {
        public string Path { get; init; }
        public long FileSizeBytes { get; init; }

        public short Channels { get; init; }
        public int SampleRate { get; init; }
        public short BitsPerSample { get; init; }
        public double DurationSeconds { get; init; }
    }
}

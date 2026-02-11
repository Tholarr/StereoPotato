namespace StereoPotato.Models
{
    public class AudioSamples
    {
        public float[] Left { get; }
        public float[] Right { get; }

        public AudioSamples(float[] left, float[] right)
        {
            Left = left;
            Right = right;
        }
    }
}

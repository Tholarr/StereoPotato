// using Avalonia;
// using Avalonia.Controls;
// using Avalonia.Media;
// using System;

// namespace StereoPotato.Controls
// {
//     public class WaveformControl : Control
//     {
//         public static readonly StyledProperty<float[]?> SamplesProperty =
//             AvaloniaProperty.Register<WaveformControl, float[]?>(nameof(Samples));

//         public float[]? Samples
//         {
//             get => GetValue(SamplesProperty);
//             set => SetValue(SamplesProperty, value);
//         }

//         // Override OnPropertyChanged to catch Samples changes
//         protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
//         {
//             base.OnPropertyChanged(change);

//             if (change.Property == SamplesProperty)
//             {
//                 // Force redraw when Samples change
//                 InvalidateVisual();
//             }
//         }

//         public override void Render(DrawingContext context)
//         {
//             base.Render(context);

//             if (Samples == null || Samples.Length == 0)
//             {
//                 Console.WriteLine("Something's wrong with the samples");
//                 return;
//             }
//             else
//             {
//                 Console.WriteLine("Everything's fine with the samples");
//             }

//             var bounds = Bounds;

//             // Background
//             context.FillRectangle(Brushes.Black, bounds);

//             var pen = new Pen(Brushes.Lime, 1);

//             double midY = bounds.Height / 2;
//             double scaleY = bounds.Height / 2;

//             int step = Math.Max(1, Samples.Length / (int)bounds.Width);

//             for (int x = 0; x < bounds.Width; x++)
//             {
//                 int index = x * step;
//                 if (index >= Samples.Length)
//                     break;

//                 float sample = Samples[index];
//                 double y = midY - sample * scaleY;

//                 context.DrawLine(
//                     pen,
//                     new Point(x, midY),
//                     new Point(x, y)
//                 );
//             }
//         }
//     }
// }

// ------------------------------------------------------------------------------------------------------------

// using Avalonia;
// using Avalonia.Controls;
// using Avalonia.Media;
// using System;

// namespace StereoPotato.Controls
// {
//     public class WaveformControl : Control
//     {
//         public static readonly StyledProperty<float[]?> SamplesProperty =
//             AvaloniaProperty.Register<WaveformControl, float[]?>(nameof(Samples));

//         public float[]? Samples
//         {
//             get => GetValue(SamplesProperty);
//             set => SetValue(SamplesProperty, value);
//         }

//         protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
//         {
//             base.OnPropertyChanged(change);

//             if (change.Property == SamplesProperty)
//                 InvalidateVisual();
//         }

//         public override void Render(DrawingContext context)
//         {
//             base.Render(context);

//             if (Samples == null || Samples.Length == 0)
//                 return;

//             var bounds = Bounds;
//             context.FillRectangle(Brushes.Black, bounds);

//             var pen = new Pen(Brushes.Lime, 1);

//             double midY = bounds.Height / 2;
//             double scaleY = bounds.Height / 2;

//             int step = Math.Max(1, Samples.Length / (int)bounds.Width);

//             for (int x = 0; x < bounds.Width; x++)
//             {
//                 int index = x * step;
//                 if (index >= Samples.Length)
//                     break;

//                 float sample = Samples[index];
//                 double y = midY - sample * scaleY;

//                 context.DrawLine(
//                     pen,
//                     new Point(x, midY),
//                     new Point(x, y)
//                 );
//             }
//         }
//     }
// }

// ------------------------------------------------------------------------------------------------------------

using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using StereoPotato.Models;
using System;

namespace StereoPotato.Controls
{
    public class WaveformControl : Control
    {
        public static readonly StyledProperty<AudioSamples?> SamplesProperty =
            AvaloniaProperty.Register<WaveformControl, AudioSamples?>(nameof(Samples));

        public AudioSamples? Samples
        {
            get => GetValue(SamplesProperty);
            set => SetValue(SamplesProperty, value);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == SamplesProperty)
                InvalidateVisual();
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            if (Samples == null)
                return;

            var bounds = Bounds;
            context.FillRectangle(Brushes.Black, bounds);

            double halfHeight = bounds.Height / 2;

            DrawChannel(
                context,
                Samples.Left,
                new Rect(0, 0, bounds.Width, halfHeight),
                Brushes.Lime
            );

            DrawChannel(
                context,
                Samples.Right,
                new Rect(0, halfHeight, bounds.Width, halfHeight),
                Brushes.Cyan
            );
        }

        private void DrawChannel(
            DrawingContext context,
            float[] samples,
            Rect area,
            IBrush brush)
        {
            if (samples.Length == 0)
                return;

            var pen = new Pen(brush, 1);

            double midY = area.Y + area.Height / 2;
            double scaleY = area.Height / 2;

            int step = Math.Max(1, samples.Length / (int)area.Width);

            for (int x = 0; x < area.Width; x++)
            {
                int index = x * step;
                if (index >= samples.Length)
                    break;

                float sample = samples[index];
                double y = midY - sample * scaleY;

                context.DrawLine(
                    pen,
                    new Point(area.X + x, midY),
                    new Point(area.X + x, y)
                );
            }
        }
    }
}

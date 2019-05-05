using System;
using System.IO;
using System.Reflection;
using Mixin;
using Symphony.Core;

using Veldrid;

namespace Composer
{
    class Program
    {
        static void Main(string[] args)
        {
            var exports = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
                .FindInstruments();
            foreach (var export in exports)
                Console.WriteLine(export);
        }
    }

    [Mixin(typeof(Instrument))]
    public partial struct GaussianBlur
    {
        [Input, Value(Min = 0f, Max = 32f)] public IObserver<float> BlurSize { get; set; }

        [Input] public IObserver<Texture> InputTexture { get; set; }
        [Output] public IObservable<Texture> BlurredTexture { get; }
    }

    [Mixin(typeof(Instrument))]
    public partial struct SineGenerator
    {
        [Output, Value(Min = 0f, Max = 1f)] public IObservable<float> Output { get; set; }
        [Input] public IObserver<float> Baseline { get; set; }
        [Input] public IObserver<float> Amplitude { get; set; }
        [Input] public IObserver<float> Phase { get; set; }
        [Input] public IObserver<float> Frequency { get; set; }
    }

    [Mixin(typeof(Instrument))]
    public partial struct TextureSink
    {
        [Input] public IObserver<Texture> Source { get; set; }
        [Output] public Stream Sink { get; set; }
    }
}

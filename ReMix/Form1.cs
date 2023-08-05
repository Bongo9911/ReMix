using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using ReMix.Models;
using System.IO;

namespace ReMix
{
    public partial class Form1 : Form
    {
        CancellationToken _cancellationToken;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ShiftAudio();
            MixAudio();
        }

        private void ShiftAudio()
        {
            string inPath = @"C:\Users\dawso\repos\ReMix\ReMix\Audio\Rick Astley - Never Gonna Give You Up (Vocals Only).mp3";
            //Every tone raise raised by one note (e.g. C goes to D, then to E)
            //Raising by a semi-tone makes it a sharp (#) and lowering by a semi-tone makes it a flat (b)
            double semitone = Math.Pow(2, 1.0 / 12);
            double upOneTone = semitone * semitone;
            double downOneTone = 1.0 / upOneTone;
            using (MediaFoundationReader reader = new MediaFoundationReader(inPath))
            {
                SmbPitchShiftingSampleProvider pitch = new SmbPitchShiftingSampleProvider(reader.ToSampleProvider());
                using (WaveOutEvent device = new WaveOutEvent())
                {
                    pitch.PitchFactor = (float)downOneTone; // or downOneTone
                                                            // just playing the first 10 seconds of the file
                    device.Init(pitch.Take(TimeSpan.FromSeconds(100)));
                    //10% volume
                    device.Volume = .1F;
                    device.Play();
                    while (device.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(500);
                    }
                }
            }
        }

        private async void MixAudio()
        {
            _cancellationToken = new CancellationToken();

            using(SongStem leadStem = new SongStem())
            using(SongStem loopStem = new SongStem())
            using(SongStem beatStem = new SongStem())
            using(SongStem bassStem = new SongStem())
            {
                leadStem.AddFile(@"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Vocals.flac");
                leadStem.AddFile(@"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Backing.flac");
                loopStem.AddFile(@"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Guitar.flac");
                loopStem.AddFile(@"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Keys.flac");
                beatStem.AddFile(@"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Snare.flac");
                beatStem.AddFile(@"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Kick (Mono).flac");
                beatStem.AddFile(@"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Cymbals.flac");
                bassStem.AddFile(@"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Bass (Mono).flac");

                List<ISampleProvider> sampleProviders = new List<ISampleProvider>();
                sampleProviders.AddRange(leadStem.SampleProviders);
                sampleProviders.AddRange(loopStem.SampleProviders);
                sampleProviders.AddRange(beatStem.SampleProviders);
                sampleProviders.AddRange(bassStem.SampleProviders);

                MixingSampleProvider mixer = new MixingSampleProvider(sampleProviders);
                using (WaveOutEvent device = new WaveOutEvent())
                {
                    device.Init(mixer.Take(TimeSpan.FromSeconds(100)));
                    //10% volume
                    device.Volume = .1F;
                    device.Play();
                    while (device.PlaybackState == PlaybackState.Playing && !_cancellationToken.IsCancellationRequested)
                    {
                        Thread.Sleep(500);
                    }
                    device.Stop();
                }
            }

            //string bassPath = @"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Bass (Mono).flac";
            //string beatPath = @"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Snare.flac";
            //string beat2Path = @"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Kick (Mono).flac";
            //string beat3Path = @"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Cymbals.flac";
            //string leadPath = @"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Vocals.flac";
            //string loopPath = @"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Guitar.flac";

            //using(AudioFileReader bassReader = new AudioFileReader(bassPath))
            //using(AudioFileReader beatReader = new AudioFileReader(beatPath))
            //using(AudioFileReader beat2Reader = new AudioFileReader(beat2Path))
            //using(AudioFileReader beat3Reader = new AudioFileReader(beat3Path))
            //using(AudioFileReader leadReader = new AudioFileReader(leadPath))
            //using(AudioFileReader loopReader = new AudioFileReader(loopPath))
            //{
            //    MonoToStereoSampleProvider stereoBass = new MonoToStereoSampleProvider(bassReader);
            //    IWaveProvider bassWaveProvider = stereoBass.ToWaveProvider16();
            //    ISampleProvider bassSampleProvider = bassWaveProvider.ToSampleProvider();

            //    MonoToStereoSampleProvider stereoBeat2 = new MonoToStereoSampleProvider(beat2Reader);
            //    IWaveProvider beat2WaveProvider = stereoBeat2.ToWaveProvider16();
            //    ISampleProvider beat2SampleProvider = beat2WaveProvider.ToSampleProvider();
            //    //beatReader.Position = beatReader.WaveFormat.AverageBytesPerSecond * 22;
            //    //loopReader.Position = (int)Math.Round(loopReader.WaveFormat.AverageBytesPerSecond * 7.5);
            //    //beatReader.Volume = 0.1F;
            //    //leadReader.Volume = 0.1F;
            //    MixingSampleProvider mixer = new MixingSampleProvider(new[] { leadReader, loopReader, beatReader, bassSampleProvider, beat2SampleProvider, beat3Reader });
            //    using (WaveOutEvent device = new WaveOutEvent())
            //    {
            //        device.Init(mixer.Take(TimeSpan.FromSeconds(100)));
            //        //10% volume
            //        device.Volume = .1F;
            //        device.Play();
            //        while (device.PlaybackState == PlaybackState.Playing && !_cancellationToken.IsCancellationRequested)
            //        {
            //            Thread.Sleep(500);
            //        }
            //        device.Stop();
            //    }
            //}
        }
    }
}
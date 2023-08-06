using NAudio.Utils;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using ReMix.Models;
using System.ComponentModel;
using System.IO;

namespace ReMix
{
    public partial class Form1 : Form
    {
        CancellationToken _cancellationToken;
        bool stateChanged = false;
        public Form1()
        {
            InitializeComponent();
            musicBackgroundWorker.WorkerSupportsCancellation = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ShiftAudio();
            MixAudio();
        }

        private void ShiftAudio()
        {
            string inPath = @"C:\Users\dawso\repos\ReMix\ReMix\Audio\Never Gonna Give You Up\Vocals.flac";
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

            //using (Song song = new Song("Never Gonna Give You Up"))
            using (Song song = new Song("Cant Feel My Face"))
            {
                MixingSampleProvider mixer = new MixingSampleProvider(song.GetSampleProviders());
                using (WaveOutEvent device = new WaveOutEvent())
                {
                    //device.Init(mixer.Skip(TimeSpan.FromSeconds(20)).Take(TimeSpan.FromSeconds(100)));
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
        }

        private void checkBoxLead_CheckedChanged(object sender, EventArgs e)
        {
            stateChanged = true;
        }

        private void checkBoxLoop_CheckedChanged(object sender, EventArgs e)
        {
            stateChanged = true;
        }

        private void checkBoxBeat_CheckedChanged(object sender, EventArgs e)
        {
            stateChanged = true;
        }

        private void checkBoxBass_CheckedChanged(object sender, EventArgs e)
        {
            stateChanged = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //TODO: get bpm per song
            //int bpm = 112;
            int bpm = 108;

            double beatSeconds = bpm / 60D;
            double secondsPerBeat = 60D / bpm;
            double startOffsetMilliseconds = 48.75 * 1000;

            double endPosition = 0;
            BackgroundWorker worker = sender as BackgroundWorker;

            while (!worker.CancellationPending)
            {
                using (Song song = new Song("Cant Feel My Face"))
                {
                    List<ISampleProvider> sampleProviders = new List<ISampleProvider>();
                    if (checkBoxLead.Checked)
                    {
                        ISampleProvider leadSample = song.LeadStem.GetSampleProvider();
                        leadSample = leadSample.Skip(TimeSpan.FromMilliseconds(endPosition + startOffsetMilliseconds));
                        sampleProviders.Add(leadSample);
                    }
                    if (checkBoxLoop.Checked)
                    {
                        ISampleProvider loopSample = song.LoopStem.GetSampleProvider();
                        loopSample = loopSample.Skip(TimeSpan.FromMilliseconds(endPosition + startOffsetMilliseconds));
                        sampleProviders.Add(loopSample);
                    }
                    if (checkBoxBeat.Checked)
                    {
                        ISampleProvider beatSample = song.BeatStem.GetSampleProvider();
                        beatSample = beatSample.Skip(TimeSpan.FromMilliseconds(endPosition + startOffsetMilliseconds));
                        sampleProviders.Add(beatSample);
                    }
                    if (checkBoxBass.Checked)
                    {
                        ISampleProvider bassSample = song.BassStem.GetSampleProvider();
                        bassSample = bassSample.Skip(TimeSpan.FromMilliseconds(endPosition + startOffsetMilliseconds));
                        sampleProviders.Add(bassSample);
                    }

                    if (sampleProviders.Count() > 0)
                    {
                        MixingSampleProvider mixer = new MixingSampleProvider(sampleProviders);
                        using (WaveOutEvent device = new WaveOutEvent())
                        {
                            device.Init(mixer);
                            //10% volume
                            device.Volume = .1F;
                            device.Play();
                            while (device.PlaybackState == PlaybackState.Playing && !worker.CancellationPending)
                            {
                                if (device.GetPositionTimeSpan().TotalSeconds + (endPosition / 1000) >= secondsPerBeat * 120)
                                {
                                    endPosition = 0;
                                    stateChanged = false;
                                    break;
                                }

                                if (stateChanged)
                                {
                                    endPosition += device.GetPositionTimeSpan().TotalMilliseconds;
                                    stateChanged = false;
                                    break;
                                }
                                Thread.Sleep(1);
                            }
                            device.Stop();
                        }
                    }
                    else
                    {
                        Thread.Sleep((int)Math.Round(beatSeconds * 1000));
                    }
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (musicBackgroundWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                musicBackgroundWorker.RunWorkerAsync();
            }
            else
            {
                musicBackgroundWorker.CancelAsync();
            }
        }
    }
}
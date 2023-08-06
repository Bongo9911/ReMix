using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReMix.Models
{
    internal class SongStem : IDisposable
    {
        private List<string> FilePaths = new List<string>();
        public List<ISampleProvider> SampleProviders { get; } = new List<ISampleProvider>();
        private List<AudioFileReader> AudioFileReaders = new List<AudioFileReader>();

        public SongStem() { }

        public SongStem(List<string> filePaths)
        {
            AddFiles(filePaths);
        }

        public SongStem(string folderTitle, string category)
        {
            string folder = "Audio/" + folderTitle + "/" + category;

            AddFiles(Directory.GetFiles(folder).ToList());
        }

        public void Dispose()
        {
            AudioFileReaders.ForEach(r => r.Dispose());
        }

        public void AddFile(string path)
        {
            FilePaths.Add(path);
            CreateSampleProvider(path);
        }

        public void AddFiles(List<string> paths)
        {
            paths.ForEach(p => AddFile(p));
        }

        private void CreateSampleProvider(string path)
        {
            AudioFileReader reader = new AudioFileReader(path);
            AudioFileReaders.Add(reader);

            //1 channel = Mono, 2 = Stereo
            if (reader.WaveFormat.Channels == 1)
            {
                //Convert from Mono to Stereo
                MonoToStereoSampleProvider stereo = new MonoToStereoSampleProvider(reader);
                IWaveProvider waveProvider = stereo.ToWaveProvider16();
                ISampleProvider sampleProvider = waveProvider.ToSampleProvider();
                SampleProviders.Add(sampleProvider);
            }
            else
            {
                SampleProviders.Add(reader);
            }
        }

        public ISampleProvider GetSampleProvider()
        {
            MixingSampleProvider mixer = new MixingSampleProvider(SampleProviders);
            return mixer;
        }
    }
}

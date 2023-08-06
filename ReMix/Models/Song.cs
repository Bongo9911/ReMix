using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReMix.Models
{
    internal class Song : IDisposable
    {
        public SongStem LeadStem { get; private set; }
        public SongStem LoopStem { get; private set; }
        public SongStem BeatStem { get; private set; }
        public SongStem BassStem { get; private set; } 

        public string FolderTitle { get; }

        public Song(string folderTitle)
        {
            FolderTitle = folderTitle;
            LoadStems();
        }

        public void Dispose()
        {
            LeadStem.Dispose();
            LoopStem.Dispose();
            BeatStem.Dispose();
            BassStem.Dispose();
        }

        private void LoadStems()
        {
            LeadStem = new SongStem(GetAudioFilePaths("Lead"));
            LoopStem = new SongStem(GetAudioFilePaths("Loop"));
            BeatStem = new SongStem(GetAudioFilePaths("Beat"));
            BassStem = new SongStem(GetAudioFilePaths("Bass"));
        }

        private List<string> GetAudioFilePaths(string category)
        {
            string folder = "Audio/" + FolderTitle + "/" + category;

            return Directory.GetFiles(folder).ToList();
        }

        public List<ISampleProvider> GetSampleProviders()
        {
            List<ISampleProvider> sampleProviders = new List<ISampleProvider>();
            sampleProviders.AddRange(LeadStem.SampleProviders);
            sampleProviders.AddRange(LoopStem.SampleProviders);
            sampleProviders.AddRange(BeatStem.SampleProviders);
            sampleProviders.AddRange(BassStem.SampleProviders);
            return sampleProviders;
        }
    }
}

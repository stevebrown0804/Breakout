using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout.Subsystems
{
 
    public class AudioPlayer
    {
        Song song;

        internal void loadContent(ContentManager contentManager)
        {
            song = contentManager.Load<Song>("BGM/BGM");
        }

        internal void PlayBGM()
        {
            MediaPlayer.Play(song);
        }

        internal void PauseBGM()
        {
            MediaPlayer.Pause();
        }

        internal void ResumeBGM()
        {
            MediaPlayer.Resume();
        }
        

        internal void StopBGM()
        {
            MediaPlayer.Stop();
        }
    }
}

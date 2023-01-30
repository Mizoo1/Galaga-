// Muaaz Bdear
// Matrikal-Nr.: 5194038
// Nour Ahmad
// Matrikal-Nr.: 5200991
// Galaga – Game Implementation

/**========================================================================
 * @file      Music.cs
 * @author    Muaaz Bdear, Nour Ahmad
 * @email     mbdear@stud.hs-bremen.de, Muaaz
 * @email     nahmed@stud.hs-bremen.de, Nour
 * @createdOn 18.01.2023
 * @version   1.0.0
 *========================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaga.Menu;
using SDL2;
using static SDL2.SDL;
using static SDL2.SDL_mixer;

namespace Galaga.Utility
{
    class Music
    {
        private IntPtr _music;
        private readonly string _file;
        public bool music_on;

        public Music(string file)
        {
            _file = file;
            Init();
            LoadMedia();
        }

        public void Play()
        {
            if (SoundMenu.music_on)
            {
                Mix_PlayMusic(_music, 0);
            }

        }
        public void Play(int i)
        {
            Mix_PlayMusic(_music, 0);
        }

        public void Stop()
        {
            Mix_HaltMusic();
        }


        private void Init()
        {
            if (SDL_Init(SDL_INIT_VIDEO | SDL_INIT_AUDIO) < 0)
            {
                throw new Exception("Failed to initialize SDL");
            }

            if (Mix_OpenAudio(44100, MIX_DEFAULT_FORMAT, 1, 1024) < 0)
            {
                throw new Exception("Failed to open audio");
            }
        }

        private void LoadMedia()
        {
            _music = Mix_LoadMUS(_file);

            if (_music == IntPtr.Zero)
            {
                throw new Exception("Failed to load _music");
            }
        }
    }
}

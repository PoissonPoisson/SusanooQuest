using SFML.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ITI.SusanooQuest.UI
{   
    class SoundManager : IDisposable
    {
        static SoundManager _instance;

        readonly Sound _soundOfPlayerProjectil;
        readonly Sound _soundOfExplosion;
        readonly Sound _soundOfTheCreationOfAnEnnemy;
        readonly Sound _soundOfAHit;
        int _currentmusic;
        List<Music> _musiclist;
        bool _isPlayingMusic;

        public static SoundManager GetInstance()
        {
            if(_instance == null) _instance = new SoundManager();
            return _instance;
        }
        

        SoundManager()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            SoundBuffer soundOfplayerProjectil = new SoundBuffer(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.piiiou.wav"));
            _soundOfPlayerProjectil = new Sound(soundOfplayerProjectil);
            /*SoundBuffer soundOfExplosion = new SoundBuffer(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources..wav"));
            _soundOfExplosion = new Sound(soundOfExplosion);
            SoundBuffer soundOfTheCreationOfAnEnnemy = new SoundBuffer(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources..wav"));
            _soundOfTheCreationOfAnEnnemy = new Sound(soundOfTheCreationOfAnEnnemy);*/
            SoundBuffer soundOfAHit = new SoundBuffer(currentAssembly.GetManifestResourceStream("ITI.SusanooQuest.UI.Resources.steve-old-hurt-sound.wav"));
            _soundOfAHit = new Sound(soundOfAHit);

            _musiclist = new List<Music>()
            {
                GetMusic("ITI.SusanooQuest.UI.Resources.Lullaby_of_Deserted_Hell.wav"),                                   // 0 : MainMenu
                GetMusic("ITI.SusanooQuest.UI.Resources.y2mate.com-elektronomia_sky_high_ncs_release_TW9d8vYrVFQ.wav"),   // 1 : Options Menu
                GetMusic("ITI.SusanooQuest.UI.Resources.warriyo-mortals-feat-laura-brehm-ncs-release.wav"),               // 2 : CreditMenu
                GetMusic("ITI.SusanooQuest.UI.Resources.sad-piano-ncs-uncopyright-music.wav"),                            // 3 : GameOverPage
                GetMusic("ITI.SusanooQuest.UI.Resources.y2mate.com-neo_tokyo_cyberpunk_mix_7JqKRqOmzi0.wav")             // 4 : LevelOneMenu
               /* "ITI.SusanooQuest.UI.Resources..wav"     */                                                   // 5 : LevelTwo Menu
            };        
        }

        Music GetMusic(string resourceName)
        {
            Music music = new Music(typeof(SoundManager).Assembly.GetManifestResourceStream(resourceName));
            music.Loop = true;
            return music;
        }

        public void Shoot()
        {            
            _soundOfPlayerProjectil.Loop = true;
            _soundOfPlayerProjectil.Play();
        }
        public void StopShoot()
        {
            _soundOfPlayerProjectil.Stop();
        }

        public void Explode()
        {
            _soundOfExplosion.Play();
        }

        public void Spawn()
        {
            _soundOfTheCreationOfAnEnnemy.Play();
        }

        public void HitSound()
        {
            _soundOfAHit.Play();
        }
        public void LaunchMusic(short nbMusic)
        {
            if (nbMusic > _musiclist.Count) throw new IndexOutOfRangeException("there is only 6 music");
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            DateTime changemusic = DateTime.Now;
            //verifier msuique courante
            if (_isPlayingMusic) StopMusic();

            //instancier musique 

            _currentmusic = nbMusic;
            _musiclist[nbMusic].Play();
            _isPlayingMusic = true;
            
            
            //lancer musique 
        }
        public void StopMusic()
        {
            // _currentmusic.Volume = 80;
            _musiclist[_currentmusic].Stop();
            _isPlayingMusic = false;
            
        }

        public void Dispose()
        {
            foreach (Music m in _musiclist) m.Dispose();
        }
    }
}

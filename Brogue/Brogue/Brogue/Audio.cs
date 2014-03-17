using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Brogue
{

    public class audioFile
    {
        public string keyName;
        public bool shouldLoop;
        public SoundEffect file;
        public SoundEffectInstance soundEngineInstance;

        public audioFile(SoundEffect newFile, string name, bool Loop = false)
        {
            file = newFile;
            keyName = name;
            shouldLoop = Loop;
        }

        public void playFile(float volume = 0.75f)
        {
            soundEngineInstance = file.CreateInstance();
            soundEngineInstance.Volume = volume;
            soundEngineInstance.IsLooped = shouldLoop;
            soundEngineInstance.Play();
        }

        public void stop()
        {
            if (soundEngineInstance != null)
            {
                soundEngineInstance.Stop();
            }
        }

    }

    public class musicFile
    {
        public string keyName;
        public bool shouldLoop;
        public Song songFile;

        public musicFile(Song newFile, string name, bool Loop = true)
        {
            songFile = newFile;
            keyName = name;
            shouldLoop = Loop;
        }

        public void playFile(float volume = 0.75f)
        {
            MediaPlayer.Play(songFile);
            MediaPlayer.IsRepeating = shouldLoop;
        }

        public void stop()
        {
            MediaPlayer.Stop();
        }
    }

    public static class Audio
    {
        private static int MAX_LIABBARY_SIZE = 30;
        private static Random random = new Random();

        private static int DEALY_MAX_DEFALT_TIME = 300;
        private static int DEALY_MIN_DEFALT_TIME = 10;
        private static int delay;
        private static int randomSoundSize = 3;

        private static musicFile[] musicLibary;
        private static audioFile[] sound;
        private static audioFile defualtSound;

        private static void backgroundSound()
        {
            if (delay <= 0)
            {
                playRandomSound();
                delay = random.Next(DEALY_MIN_DEFALT_TIME, DEALY_MAX_DEFALT_TIME);
            }
            else if (delay > 0)
            {
                delay -= 1;
            }
        }

        public static void stopAllAudio()
        {
            for (int currentSlot = 0; currentSlot < MAX_LIABBARY_SIZE; currentSlot++)
            {
                if (musicLibary[currentSlot] != null)
                {
                    musicLibary[currentSlot].stop();
                }

                if (sound[currentSlot] != null)
                {
                    sound[currentSlot].stop();
                }
            }
        }

        public static void stopAllMusic()
        {
            for (int currentSlot = 0; currentSlot < MAX_LIABBARY_SIZE; currentSlot++)
            {
                if (musicLibary[currentSlot] != null)
                {
                    musicLibary[currentSlot].stop();
                }
            }
        }

        public static void stopAllSounds()
        {
            for (int currentSlot = 0; currentSlot < MAX_LIABBARY_SIZE; currentSlot++)
            {
                if (sound[currentSlot] != null)
                {
                    sound[currentSlot].stop();
                }
            }
        }

        public static void playRandomSong(float volume = 0.75f)
        {
            stopAllMusic();
            int selected = random.Next(0, MAX_LIABBARY_SIZE); ;
            while (musicLibary[selected] == null)
            {
                selected = random.Next(0, MAX_LIABBARY_SIZE);
            }
            musicLibary[selected].playFile(volume);
        }

        public static void playRandomSound(float volume = 0.05f)
        {
            int selected = random.Next(1, randomSoundSize); ;
            while (sound[selected] == null)
            {
                selected = random.Next(2, randomSoundSize);
            }
            sound[selected].playFile(volume);
        }

        public static void playSound(String key, float volume = 0.75f)
        {
            bool fileNotFound = true;
            for (int currentSlot = 0; currentSlot < MAX_LIABBARY_SIZE && (fileNotFound); currentSlot++)
            {
                if (sound[currentSlot] != null)
                {
                    if (sound[currentSlot].keyName.Equals(key))
                    {
                        sound[currentSlot].playFile(volume);
                        fileNotFound = false;
                    }
                }
            }
            if (fileNotFound)
            {
                defualtSound.playFile();
            }
        }

        public static void playMusic(String key, float volume = 0.75f)
        {
            bool fileNotFound = true;
            for (int currentSlot = 0; currentSlot < MAX_LIABBARY_SIZE && (fileNotFound); currentSlot++)
            {
                if (musicLibary[currentSlot] != null)
                {
                    if (musicLibary[currentSlot].keyName.Equals(key))
                    {
                        musicLibary[currentSlot].playFile(volume);
                        fileNotFound = false;
                    }
                }
            }
            if (fileNotFound)
            {
                defualtSound.playFile();
            }
        }

        public static void LoadContent(ContentManager content)
        {
            // audioLibary = new audioFile[MAX_LIABBARY_SIZE];
            sound = new audioFile[MAX_LIABBARY_SIZE];
            musicLibary = new musicFile[MAX_LIABBARY_SIZE];

            //mainMenuTheme = content.Load<Song>("Audio/Stoneworld Battle");
            //Load Music
            musicLibary[0] = new musicFile(content.Load<Song>("Music/BrogueII"), "Brogue II", true);
            musicLibary[1] = new musicFile(content.Load<Song>("Music/Stoneworld Battle"), "Stoneworld Battle", true);
            musicLibary[2] = new musicFile(content.Load<Song>("Music/The Descent"), "The Descent", true);
            musicLibary[3] = new musicFile(content.Load<Song>("Music/Light in the dark"), "Light in the dark", true);
            musicLibary[4] = new musicFile(content.Load<Song>("Music/LetsRock"), "Lets Rock", true);

            //Load Sound
            defualtSound = new audioFile(content.Load<SoundEffect>("Sound/Whammy"), "Whammy");
            sound[0]     = new audioFile(content.Load<SoundEffect>("Sound/stairs"), "stairs");
            sound[1]     = new audioFile(content.Load<SoundEffect>("Sound/Chest"), "chest");
            sound[2]     = new audioFile(content.Load<SoundEffect>("Sound/Water_Drop"), "waterDrop");
            sound[3]     = new audioFile(content.Load<SoundEffect>("Sound/door"), "door");
            sound[4]     = new audioFile(content.Load<SoundEffect>("Sound/switch"), "switch");
            sound[5]     = new audioFile(content.Load<SoundEffect>("Sound/ironGate"), "IronGate");
            sound[6]     = new audioFile(content.Load<SoundEffect>("Sound/Fireball"), "Fireball");
            sound[7]     = new audioFile(content.Load<SoundEffect>("Sound/whoosh"), "whoosh");
            sound[8]     = new audioFile(content.Load<SoundEffect>("Sound/swordSlash"), "Slash");
            sound[9]     = new audioFile(content.Load<SoundEffect>("Sound/slashAttack"), "swordAttack");
            sound[10]    = new audioFile(content.Load<SoundEffect>("Sound/lightningStorm"), "lightning");
            sound[11]    = new audioFile(content.Load<SoundEffect>("Sound/DaggerStab"), "DaggerStab");
            sound[12]    = new audioFile(content.Load<SoundEffect>("Sound/Punch"), "Mugging");
            sound[13]    = new audioFile(content.Load<SoundEffect>("Sound/smokePoof"), "Smoke");
            sound[14]    = new audioFile(content.Load<SoundEffect>("Sound/HammerSmash"), "HammerSmash");
            sound[15]    = new audioFile(content.Load<SoundEffect>("Sound/bloodSpit"), "bloodSpit");
            sound[16]    = new audioFile(content.Load<SoundEffect>("Sound/enimeFireball"), "enimeFireball");
            sound[17]    = new audioFile(content.Load<SoundEffect>("Sound/SwordClash"), "SwordClash");
            sound[18]    = new audioFile(content.Load<SoundEffect>("Sound/ArrowShot"), "ArrowShot");
            sound[19]    = new audioFile(content.Load<SoundEffect>("Sound/Parry"), "Parry");
            sound[20]    = new audioFile(content.Load<SoundEffect>("Sound/Gunshot"), "Gunshot");
            sound[21]    = new audioFile(content.Load<SoundEffect>("Sound/eviscerate"), "eviscerate");
            sound[22]    = new audioFile(content.Load<SoundEffect>("Sound/Wind"), "WhirlwindSlash");
            sound[23]    = new audioFile(content.Load<SoundEffect>("Sound/Arcane"), "Arcane");
            sound[24]    = new audioFile(content.Load<SoundEffect>("Sound/Jump"), "Jump");
            sound[25]    = new audioFile(content.Load<SoundEffect>("Sound/SniperShot"), "SniperShot");
            sound[26]    = new audioFile(content.Load<SoundEffect>("Sound/ClockTick"), "ClockTick");
            sound[27]    = new audioFile(content.Load<SoundEffect>("Sound/Poison"), "Poison");
            sound[28]    = new audioFile(content.Load<SoundEffect>("Sound/Siphon"), "Siphon");


            //defualt
            delay = random.Next(DEALY_MIN_DEFALT_TIME, DEALY_MAX_DEFALT_TIME);
        }

        public static void update()
        {
           backgroundSound();
        }

    }
}
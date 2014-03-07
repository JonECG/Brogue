using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

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

    public static class Audio
    {
        private static int MAX_LIABBARY_SIZE = 10;
        private static Random random = new Random();

        private static int DEALY_MAX_DEFALT_TIME = 300;
        private static int DEALY_MIN_DEFALT_TIME = 10;
        private static int delay;
        
        private static audioFile[] music;
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
                if (music[currentSlot] != null)
                {
                    music[currentSlot].stop();
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
                if (music[currentSlot] != null)
                {
                    music[currentSlot].stop();
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
            while (music[selected] == null)
            {
                selected = random.Next(0, MAX_LIABBARY_SIZE);
            }
            music[selected].playFile(volume);
        }

        public static void playRandomSound(float volume = 0.05f)
        {
            int selected = random.Next(1, MAX_LIABBARY_SIZE); ;
            while (sound[selected] == null)
            {
                selected = random.Next(2, MAX_LIABBARY_SIZE);
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
                if (music[currentSlot] != null)
                {
                    if (music[currentSlot].keyName.Equals(key))
                    {
                        music[currentSlot].playFile(volume);
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
            music = new audioFile[MAX_LIABBARY_SIZE];
            sound = new audioFile[MAX_LIABBARY_SIZE];

            //Load Music
            music[0] = new audioFile(content.Load<SoundEffect>("Music/BrogueII"), "Brogue II", true);
            music[1] = new audioFile(content.Load<SoundEffect>("Music/The_Thing"), "The_Thing", true);
            music[2] = new audioFile(content.Load<SoundEffect>("Music/E1M1"), "Doom", true);
            //music[3] = new audioFile(content.Load<SoundEffect>("Music/SOMITEST"), "Monkey Island", true);

            //Load Sound
            defualtSound = new audioFile(content.Load<SoundEffect>("Sound/Whammy"), "Whammy");
            sound[0] = new audioFile(content.Load<SoundEffect>("Sound/stairs"), "stairs");
            sound[1] = new audioFile(content.Load<SoundEffect>("Sound/Chest"), "chest");
            sound[2] = new audioFile(content.Load<SoundEffect>("Sound/Water_Drop"), "waterDrop");
            sound[3] = new audioFile(content.Load<SoundEffect>("Sound/door"), "door");
            sound[4] = new audioFile(content.Load<SoundEffect>("Sound/switch"), "switch");
            sound[5] = new audioFile(content.Load<SoundEffect>("Sound/stairs"), "stairs");

            //defualt
            delay = random.Next(DEALY_MIN_DEFALT_TIME, DEALY_MAX_DEFALT_TIME);
        }

        public static void update()
        {
           backgroundSound();
        }

    }
}

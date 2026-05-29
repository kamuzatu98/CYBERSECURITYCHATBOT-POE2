using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CybersecurityBotWPF
{
    public static class AudioManager
    {
        public static void PlayGreeting()
        {
            try
            {
                if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return;

                string audioPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "assign1234.wav");

                if (File.Exists(audioPath))
                {
                    using var player = new System.Media.SoundPlayer(audioPath);
                    player.Play();
                }
            }
            catch { }
        }
    }
}
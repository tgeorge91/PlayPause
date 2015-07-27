using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PlayPause
{
    public static class Program
    {
        private static string MediaPlayer = "iTunes";
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;
        private const int APPCOMMAND_MEDIA_PLAY_PAUSE = 0xE0000;

        static void Main()
        {
            KeyListener.Start();
        }

        public static void SendToApp(string key)
        {
            //Console.WriteLine(key);
            var window = Window.GetActiveProcessFileName();
            Console.WriteLine(window);
        }
    }
}
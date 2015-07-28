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
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        static void Main()
        {
            KeyListener.Start();
        }

        public static void SendToApp(string key)
        {
            var mediaPlayer = GetMediaPlayer();
            var activeWindow = Window.GetActiveProcess();

            var mediaPlayerProcess = Process.GetProcessesByName(mediaPlayer).FirstOrDefault();
            var mediaPlayerHandle = mediaPlayerProcess.MainWindowHandle;

            SetForegroundWindow(mediaPlayerHandle);
            SendKeys.Send(key);
            SetForegroundWindow(activeWindow.MainWindowHandle);
        }

        public static string GetMediaPlayer()
        {
            return "iTunes";
        }
    }
}
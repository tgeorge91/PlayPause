using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Principal;
using System.Management;
using System.ComponentModel;
using System.Security.Permissions;

namespace PlayPause
{
    public static class Window
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        //[DllImport("user32.dll")]
        //static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        //public static string GetActiveWindowTitle()
        //{
        //    const int nChars = 256;
        //    StringBuilder Buff = new StringBuilder(nChars);
        //    IntPtr handle = GetForegroundWindow();

        //    if (GetWindowText(handle, Buff, nChars) > 0)
        //    {
        //        return Buff.ToString();
        //    }
        //    return null;
        //}

        public static Process GetActiveProcess()
        {
            IntPtr hwnd = GetForegroundWindow();

            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            var process = Process.GetProcessById((int)pid);

            return process;
        }
    }
}
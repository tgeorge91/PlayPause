using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

/*
 *  Giving credit where credit is due:
 *  http://stackoverflow.com/questions/7181978/special-keys-on-keyboards
 *  http://stackoverflow.com/questions/15292175/c-sharp-using-sendkey-function-to-send-a-key-to-another-application
 *  http://blogs.msdn.com/b/toub/archive/2006/05/03/589423.aspx
 *  https://social.msdn.microsoft.com/Forums/en-US/1c47525d-5232-43ee-abda-0844330c6f3d/how-to-use-getforegroundwindow-to-get-newly-launched-application-windows-instance-in-c?forum=csharplanguage
 *  http://stackoverflow.com/questions/777548/how-do-i-determine-the-owner-of-a-process-in-c 
 *  http://stackoverflow.com/questions/31663744/find-out-if-another-application-is-running-as-admin/ (my question)
 * 
 * 
 */

namespace PlayPause
{
    public class KeyListener
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static List<string> KeysToAccept = new List<string> { "MediaPlayPause", "MediaPreviousTrack", "MediaNextTrack", "MediaStop" };

        public static void Start()
        {
            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                string key = ((Keys)vkCode).ToString();
                if (KeysToAccept.Any(q => q == key))
                {
                    Program.SendToApp(key);
                };
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MyHotKeys
{
    public sealed class Z
    {
        #region FindWindow

        public const uint WM_GETTEXT = 0x000D;
        public const uint WM_GETTEXTLENGTH = 0x000E;

        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_MAX = 11;
        public const int N_WND_CLASSNAME_LEN = 256;

        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr handle, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern IntPtr SendMessageByStringBuilder(IntPtr hWnd, uint wMsg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.Dll")]
        public static extern bool EnumWindows(EnumWindowsProc callback, IntPtr lParam);

        [DllImport("shlwapi.dll")]
        public static extern bool PathUnExpandEnvStrings(string pszPath, StringBuilder pszBuf, uint cchBuf);

        [DllImport("shell32.dll")]
        public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, uint nIconIndex);

        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hicon);

        [DllImport("kernel32.dll")]
        public static extern UIntPtr GetCurrentThreadId();

        [DllImport("user32.dll")]
        public static extern UIntPtr GetWindowThreadProcessId(IntPtr hWnd, out UIntPtr lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(UIntPtr idAttach, UIntPtr idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindowEnabled(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);

        public static bool ActivateWindow(string title, bool partialMatch, string windowClass)
        {
            IntPtr hwnd = FindWindow(title, partialMatch, windowClass);
            if (hwnd != IntPtr.Zero)
            {
                return ActivateWindow(hwnd);
            }
            return false;
        }

        public static bool ActivateWindow(IntPtr hwnd)
        {
            UIntPtr targetThreadId = GetWindowThreadProcessId(hwnd, out var pid);
            if (targetThreadId == UIntPtr.Zero)
            {
                return false;
            }

            var currentThreadId = GetCurrentThreadId();
            bool attached = AttachThreadInput(currentThreadId, targetThreadId, true);

            if (!IsWindowVisible(hwnd)) ShowWindow(hwnd, SW_SHOW);

            if (IsIconic(hwnd)) ShowWindow(hwnd, SW_RESTORE);

            bool f = SetForegroundWindow(hwnd);
            if (f) SetActiveWindow(hwnd);

            if (attached) AttachThreadInput(currentThreadId, targetThreadId, false);

            return f;
        }

        public static string GetWindowClass(IntPtr hwnd)
        {
            var sb = new StringBuilder(N_WND_CLASSNAME_LEN);
            if (GetClassName(hwnd, sb, sb.Capacity) > 0)
            {
                return sb.ToString();
            }
            return null;
        }

        public static string GetWindowText(IntPtr hwnd)
        {
            int length = SendMessage(hwnd, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero).ToInt32();
            if (length > 0)
            {
                length++;
                var sb = new StringBuilder(length);
                int n = SendMessageByStringBuilder(hwnd, WM_GETTEXT, new IntPtr(length), sb).ToInt32();
                if (n > 0)
                {
                    return sb.ToString();
                }
            }
            return null;
        }

        public static bool EnumWindowsProcImpl(IntPtr hwnd, IntPtr lParam)
        {
            var gch = GCHandle.FromIntPtr(lParam);
            var param = (object[])gch.Target;
            string title = (string)param[0];
            string windowClass = (string)param[1];

            string thisTitle = GetWindowText(hwnd);
            string thisClass = GetWindowClass(hwnd);

            if (thisTitle != null)
            {
                if (thisTitle.IndexOf(title, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    if (windowClass == null || string.Compare(thisClass, windowClass, true) == 0)
                    {
                        param[2] = hwnd;
                        return false;
                    }
                }
            }

            return true;
        }

        public static IntPtr FindWindow(string title, bool partialMatch, string windowClass)
        {
            if (!partialMatch) return FindWindow(null, title);

            var a = new object[] { title, windowClass, IntPtr.Zero };
            var gch = GCHandle.Alloc(a);
            IntPtr param = GCHandle.ToIntPtr(gch);
            try
            {
                EnumWindows(new EnumWindowsProc(EnumWindowsProcImpl), param);
            }
            finally
            {
                gch.Free();
            }

            return (IntPtr)a[2];
        }

        #endregion

        #region WM_HOTKEY

        public const uint WM_HOTKEY = 0x0312;
        public const uint MOD_ALT = 0x0001;
        public const uint MOD_CONTROL = 0x0002;
        public const uint MOD_SHIFT = 0x004;
        public const uint MOD_NOREPEAT = 0x400;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, int vk);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion

        #region NativeHelper.dll

        [DllImport("NativeHelper.dll", SetLastError = true)]
        public static extern bool SaveKeyboardState(
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 256)]byte[] buffer);

        [DllImport("NativeHelper.dll", SetLastError = true)]
        public static extern bool RestoreKeyboardState(
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 256)]byte[] buffer);

        #endregion

    }
}

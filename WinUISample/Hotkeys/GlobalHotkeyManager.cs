using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Hotkeys
{
    public class GlobalHotkeyManager
    {

        public static GlobalHotkeyManager Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GlobalHotkeyManager();
                    return _instance;
                }
                else
                {
                    return _instance;
                }
            }
        }

        private static GlobalHotkeyManager _instance;

        private GlobalHotkeyManager() { }

        private const uint WM_HOTKEY = 0x0312;

        internal delegate void HotkeyPressedHandler(HWND hwnd, uint uMsg, WPARAM wParam, LPARAM lParam);
        internal event HotkeyPressedHandler OnHotkeyPressed;

        internal bool RegisterHotkey(Window window, int hotkeyId, HOT_KEY_MODIFIERS modifiers, uint virtualKey)
        {
            HWND hwnd = new HWND(WinRT.Interop.WindowNative.GetWindowHandle(window).ToInt32());
            bool success = PInvoke.RegisterHotKey(hwnd, hotkeyId, modifiers, virtualKey);

            if (success)
            {
                if (m_windowHotkeys.ContainsKey(hwnd))
                {
                    m_windowHotkeys[hwnd].Add(hotkeyId);
                }
                else
                {
                    m_windowHotkeys[hwnd] = new HashSet<int>{ hotkeyId };
                }
            }

            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        public void RegisterWindow(Window window)
        {
            HWND hwnd = new HWND(WinRT.Interop.WindowNative.GetWindowHandle(window).ToInt32());
            if (m_windowProcs.ContainsKey(hwnd))
                return;

            WNDPROC hotKeyProc;
            hotKeyProc = HotKeyProc;
            var hotKeyProcPointer = Marshal.GetFunctionPointerForDelegate(hotKeyProc);
            WNDPROC originalWndProc = Marshal.GetDelegateForFunctionPointer<WNDPROC>((IntPtr)PInvoke.SetWindowLongPtr(hwnd, WINDOW_LONG_PTR_INDEX.GWL_WNDPROC, hotKeyProcPointer));

            m_windowProcs[hwnd] = new Tuple<WNDPROC, WNDPROC>(originalWndProc, hotKeyProc);

            window.Closed += Window_Closed;
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            Window window = sender as Window;
            if (window != null)
            {
                HWND hwnd = new HWND(WinRT.Interop.WindowNative.GetWindowHandle(window).ToInt32());

                if (m_windowProcs.TryGetValue(hwnd, out var procs))
                {
                    PInvoke.SetWindowLongPtr(hwnd, WINDOW_LONG_PTR_INDEX.GWL_WNDPROC, Marshal.GetFunctionPointerForDelegate(procs.Item1));
                    m_windowProcs.Remove(hwnd);
                }

                UnregisterAllHotkeys(window);
            }
        }

        private LRESULT HotKeyProc(HWND hwnd, uint uMsg, WPARAM wParam, LPARAM lParam)
        {
            if (uMsg == WM_HOTKEY)
            {
                OnHotkeyPressed?.Invoke(hwnd, uMsg, wParam, lParam);
                return (LRESULT)IntPtr.Zero;
            }

            WNDPROC originalWndProc = m_windowProcs[hwnd].Item1;

            return PInvoke.CallWindowProc(originalWndProc, hwnd, uMsg, wParam, lParam);
        }

        internal bool UnregisterHotkey(Window window, int hotkeyId)
        {
            HWND hwnd = new HWND(WinRT.Interop.WindowNative.GetWindowHandle(window).ToInt32());
            bool success = PInvoke.UnregisterHotKey(hwnd, hotkeyId);

            if (success && m_windowHotkeys.ContainsKey(hwnd))
            {
                if (m_windowHotkeys[hwnd].Contains(hotkeyId))
                {
                    m_windowHotkeys[hwnd].Remove(hotkeyId);
                }
            }

            return success;
        }

        internal void UnregisterAllHotkeys(Window window)
        {
            HWND hwnd = new HWND(WinRT.Interop.WindowNative.GetWindowHandle(window).ToInt32());
            if (m_windowHotkeys.ContainsKey(hwnd))
            {
                foreach(var hotkeyId in  m_windowHotkeys[hwnd])
                {
                    PInvoke.UnregisterHotKey(hwnd, hotkeyId);
                }
            }

            m_windowHotkeys.Remove(hwnd);
        }

        private Dictionary<HWND, Tuple<WNDPROC, WNDPROC>> m_windowProcs = new();
        private Dictionary<HWND, HashSet<int>> m_windowHotkeys = new();
    }
}

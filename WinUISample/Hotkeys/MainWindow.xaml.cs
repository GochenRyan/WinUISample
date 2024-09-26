using Microsoft.UI.Xaml;
using System;
using System.Runtime.InteropServices;
using Windows.Win32;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Hotkeys
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private const uint WM_HOTKEY = 0x0312;
        private const uint OemPeriod = 0xBE;
        private Windows.Win32.UI.WindowsAndMessaging.WNDPROC origPrc;
        private Windows.Win32.UI.WindowsAndMessaging.WNDPROC hotKeyPrc;


        private Windows.Win32.Foundation.LRESULT HotKeyPrc(Windows.Win32.Foundation.HWND hwnd,
            uint uMsg,
            Windows.Win32.Foundation.WPARAM wParam,
            Windows.Win32.Foundation.LPARAM lParam)
        {
            if (uMsg == WM_HOTKEY)
            {
                if (PInvoke.GetActiveWindow() == hwnd)
                {
                    CountText.Text = "Hot key pressed: " + (++m_count);
                    return (Windows.Win32.Foundation.LRESULT)IntPtr.Zero;
                }
            }

            return PInvoke.CallWindowProc(origPrc, hwnd, uMsg, wParam, lParam);
        }

        public MainWindow()
        {
            this.InitializeComponent();

            hotKeyPrc = HotKeyPrc;
            Windows.Win32.Foundation.HWND hwnd = new Windows.Win32.Foundation.HWND(WinRT.Interop.WindowNative.GetWindowHandle(this).ToInt32());

            var success = Windows.Win32.PInvoke.RegisterHotKey(hwnd, 0, Windows.Win32.UI.Input.KeyboardAndMouse.HOT_KEY_MODIFIERS.MOD_WIN | Windows.Win32.UI.Input.KeyboardAndMouse.HOT_KEY_MODIFIERS.MOD_CONTROL, OemPeriod);

            var hotKeyPrcPointer = Marshal.GetFunctionPointerForDelegate(hotKeyPrc);
            origPrc = Marshal.GetDelegateForFunctionPointer<Windows.Win32.UI.WindowsAndMessaging.WNDPROC>((IntPtr)Windows.Win32.PInvoke.SetWindowLongPtr(hwnd, Windows.Win32.UI.WindowsAndMessaging.WINDOW_LONG_PTR_INDEX.GWL_WNDPROC, hotKeyPrcPointer));
        }

        private int m_count = 0;
    }
}

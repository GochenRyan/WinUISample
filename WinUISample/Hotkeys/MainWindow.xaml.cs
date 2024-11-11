using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System.Collections.Generic;
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
        private const uint OemPeriod = 0xBE;

        private bool IsCurrentlyInputtingText()
        {
            var focusedElement = FocusManager.GetFocusedElement(this.Content.XamlRoot);
            if (focusedElement is Control control)
            {
                if (control is TextBox ||
                    control is AutoSuggestBox ||
                    control is NumberBox ||
                    control is PasswordBox ||
                    control is RichEditBox)
                {
                    return true;
                }
            }

            return false;
        }

        public MainWindow(bool IsMain = true)
        {
            this.InitializeComponent();
            
            if (IsMain)
            {
                GlobalHotkeyManager.Instance.OnHotkeyPressed += Hm_OnHotkeyPressed;
                GlobalHotkeyManager.Instance.RegisterWindow(this);
                GlobalHotkeyManager.Instance.RegisterHotkey(this, 1, Windows.Win32.UI.Input.KeyboardAndMouse.HOT_KEY_MODIFIERS.MOD_WIN | Windows.Win32.UI.Input.KeyboardAndMouse.HOT_KEY_MODIFIERS.MOD_CONTROL, OemPeriod);
            }
        }

        private void Hm_OnHotkeyPressed(Windows.Win32.Foundation.HWND hwnd, uint uMsg, Windows.Win32.Foundation.WPARAM wParam, Windows.Win32.Foundation.LPARAM lParam)
        {
            var activeWindow = PInvoke.GetActiveWindow();
            var curhwnd = new Windows.Win32.Foundation.HWND(WinRT.Interop.WindowNative.GetWindowHandle(this).ToInt32());
            if (curhwnd == activeWindow)
            {
                UpdateText();
            }
            else
            {
                foreach (var window in m_otherWindows)
                {
                    Windows.Win32.Foundation.HWND otherhwnd = new Windows.Win32.Foundation.HWND(WinRT.Interop.WindowNative.GetWindowHandle(window).ToInt32());
                    if (activeWindow == otherhwnd)
                    {
                        window.UpdateText();
                    }
                }
            }
        }

        private void UpdateText()
        {
            if (IsCurrentlyInputtingText())
            {
                CountText.Text = $"Entering text...{++m_count}";
            }
            else
            {
                CountText.Text = $"Hot key pressed: {++m_count}";
            }
        }

        private int m_count = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newWin = new MainWindow(false);
            newWin.Activate();

            m_otherWindows.Add(newWin);
            newWin.Closed += NewWin_Closed;
        }

        private void NewWin_Closed(object sender, WindowEventArgs args)
        {
            var window = sender as MainWindow;
            m_otherWindows.Remove(window);
        }

        HashSet<MainWindow> m_otherWindows = new();

        bool IsMain = true;
    }
}

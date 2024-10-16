using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
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

        public MainWindow()
        {
            this.InitializeComponent();
            var hm = GlobalHotkeyManager.Instance;
            hm.OnHotkeyPressed += Hm_OnHotkeyPressed;
            hm.RegisterWindow(this);
            hm.RegisterHotkey(this, 1, Windows.Win32.UI.Input.KeyboardAndMouse.HOT_KEY_MODIFIERS.MOD_WIN | Windows.Win32.UI.Input.KeyboardAndMouse.HOT_KEY_MODIFIERS.MOD_CONTROL, OemPeriod);
        }

        private void Hm_OnHotkeyPressed(Windows.Win32.Foundation.HWND hwnd, uint uMsg, Windows.Win32.Foundation.WPARAM wParam, Windows.Win32.Foundation.LPARAM lParam)
        {
            var curhwnd = new Windows.Win32.Foundation.HWND(WinRT.Interop.WindowNative.GetWindowHandle(this).ToInt32());
            if (curhwnd != hwnd)
                return;

            if (PInvoke.GetActiveWindow() == hwnd)
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
        }

        private int m_count = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window newWin = new MainWindow();
            newWin.Activate();
        }
    }
}

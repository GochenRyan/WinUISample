using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostWindowSample
{
    public static class WindowManager
    {
        private static Dictionary<AppWindow, Window> windowMap = new Dictionary<AppWindow, Window>();

        public static void RegisterWindow(AppWindow appWindow, Window window)
        {
            windowMap[appWindow] = window;
        }

        public static Window GetWindow(AppWindow appWindow)
        {
            windowMap.TryGetValue(appWindow, out Window window);
            return window;
        }

        public static void UnregisterWindow(AppWindow appWindow)
        {
            windowMap.Remove(appWindow);
        }
    }
}

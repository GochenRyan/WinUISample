using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SearchTree
{
    public static class WinUIExtensions
    {
        public static async Task WaitForLoadedAsync(FrameworkElement element)
        {
            var tcs = new TaskCompletionSource<object>();

            void OnLoaded(object sender, RoutedEventArgs e)
            {
                element.Loaded -= OnLoaded;
                tcs.SetResult(null);
            }

            // If the control is already loaded, complete the task immediately
            if (element.IsLoaded)
            {
                return;
            }

            // If not loaded, subscribe to the Loaded event
            element.Loaded += OnLoaded;

            // Wait for the control to load
            await tcs.Task;
        }
    }
}

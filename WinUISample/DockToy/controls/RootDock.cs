using DockToy.models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;
using System.Collections.Generic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DockToy.controls
{
    [ContentProperty(Name = "Content")]
    public sealed class RootDock : DependencyObject, IRoot
    {
        public object Content {  get; set; }
    }
}

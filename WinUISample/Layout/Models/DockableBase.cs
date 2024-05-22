using Microsoft.UI.Xaml;

namespace Layout.Models
{
    public abstract class DockableBase : DependencyObject, IDockable
    {
        public static DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(DockableBase),
            new PropertyMetadata(string.Empty));

        public static DependencyProperty FactoryProperty = DependencyProperty.Register(
            nameof(Factory),
            typeof(IFactory),
            typeof(DockableBase),
            new PropertyMetadata(null));

        public static DependencyProperty OwnerProperty = DependencyProperty.Register(
            nameof(Owner),
            typeof(IDockable),
            typeof(DockableBase),
            new PropertyMetadata(null));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public IFactory? Factory
        {
            get => (IFactory)GetValue(FactoryProperty);
            set => SetValue(FactoryProperty, value);
        }

        public IDockable Owner
        {
            get => (IDockable)GetValue(OwnerProperty);
            set => SetValue(OwnerProperty, value);
        }
    }
}

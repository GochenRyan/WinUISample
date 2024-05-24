using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomThumb
{
    [TemplatePart(Name = "InternalThumb", Type = typeof(Thumb))]
    public sealed class CustomThumb : Control
    {
        public CustomThumb()
        {
            this.DefaultStyleKey = typeof(CustomThumb);
        }

        protected override void OnApplyTemplate()
        {
            if (_thumb != null)
            {
                _thumb.DragCompleted -= Thumb_DragCompleted;
                _thumb.DragDelta -= Thumb_DragDelta;
                _thumb.DragStarted -= Thumb_DragStarted;
                _thumb.KeyDown -= Thumb_KeyDown;
            }

            _thumb = GetTemplateChild("InternalThumb") as Thumb;

            if (_thumb != null)
            {
                _thumb.DragCompleted += Thumb_DragCompleted;
                _thumb.DragDelta += Thumb_DragDelta;
                _thumb.DragStarted += Thumb_DragStarted;
                _thumb.KeyDown += Thumb_KeyDown;
            }
        }

        private void Thumb_KeyDown(object sender, KeyRoutedEventArgs e)
        {
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            ThumbDragStarted.Invoke(this, e);
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ThumbDragDelta.Invoke(this, e);
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            ThumbDragCompleted.Invoke(this, e);
        }

        public event DragStartedEventHandler ThumbDragStarted;
        public event DragCompletedEventHandler ThumbDragCompleted;
        public event DragDeltaEventHandler ThumbDragDelta;

        private Thumb _thumb;
    }
}

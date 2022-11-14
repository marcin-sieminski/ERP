using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Firma.Behaviors
{
    public class CloseWindowButtonBehavior : Behavior<Window>
    {
        public Key KeyBehavior { get; set; }

        protected override void OnAttached()
        {
            Window window = AssociatedObject;
            if (window != null) window.PreviewKeyDown += window_PreviewKeyDown;
        }

        private void window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Window window = (Window)sender;
            if (e.Key == KeyBehavior) window.Close();
        }
    }
}
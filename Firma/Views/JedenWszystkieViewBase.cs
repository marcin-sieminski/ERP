using System.Windows;
using System.Windows.Controls;

namespace Firma.Views
{
    public class JedenWszystkieViewBase : UserControl
    {
        static JedenWszystkieViewBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(JedenWszystkieViewBase), new FrameworkPropertyMetadata(typeof(JedenWszystkieViewBase)));
        }
    }
}

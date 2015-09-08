using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Scrambler.View
{
    internal static class LocalExtensions
    {
        public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action)
        {
            Window window = ((FrameworkElement)templateFrameworkElement).TemplatedParent as Window;
            if (window != null) action(window);
        }

        public static void ForTextBoxFromTemplate(this object templateFrameworkElement, Action<TextBox> action)
        {
            TextBox textBox = ((FrameworkElement)templateFrameworkElement).TemplatedParent as TextBox;
            action(textBox);
        }
    }


    public partial class AirWindow
    {
        void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.DragMove());
        }

        void btnClose_Click(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.Close());
        }

        void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.WindowState = System.Windows.WindowState.Minimized);
        }
    }
}

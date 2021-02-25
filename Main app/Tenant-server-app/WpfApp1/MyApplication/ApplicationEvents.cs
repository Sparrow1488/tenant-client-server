using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1.MyApplication
{
    public class ApplicationEvents
    {
        public void ShowExceptionMessage(string errorText, TextBlock displayElem)
        {
            displayElem.Visibility = Visibility.Visible;
            displayElem.Foreground = new SolidColorBrush(Colors.Red);
            displayElem.Text = errorText;
        }
        public void ShowEventMessage(string errorText, TextBlock displayElem)
        {
            displayElem.Visibility = Visibility.Visible;
            displayElem.Background = new SolidColorBrush(Colors.AliceBlue);
            displayElem.Text = errorText;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.MyApplication
{
    public class ApplicationEvents
    {
        public void ShowExceptionMessage(string errorText, TextBlock displayElem)
        {
            displayElem.Visibility = Visibility.Visible;
            displayElem.Text = errorText;
        }
    }
}

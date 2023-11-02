using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace PZ_18
{
    public sealed partial class Buttons : Page
    {
        public Buttons()
        {
            this.InitializeComponent();
        }

        private void bnt1_Click(object sender, RoutedEventArgs e)
        {
            btn1.Content = "💀";
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            btn2.Content = "Что это???";
        }
    }
}

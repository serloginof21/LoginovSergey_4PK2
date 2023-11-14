using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace PZ_24
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.ToString() == "Enter")
            {
                try
                {
                    webView.Navigate(new Uri("https://www." + url_txt.Text));
                }
                catch { }
            }

        }

        private void favorite_btn_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new MenuFlyoutItem();
            newItem.Text = url_txt.Text;
            newItem.Click += ItemClick;
            menu_favorite.Items.Add(newItem);
        }

        private void ItemClick(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem mfi = (MenuFlyoutItem)sender;
            url_txt.Text = mfi.Text;
            webView.Navigate(new Uri("https://www." + url_txt.Text));
        }

        private void Fade_Click(object sender, RoutedEventArgs e)
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            GradientStop stop1 = new GradientStop() { Color = Colors.LightGray, Offset = 0 };
            GradientStop stop2 = new GradientStop() { Color = Colors.White, Offset = 1 };
            gradientBrush.GradientStops.Add(stop1);
            gradientBrush.GradientStops.Add(stop2);
            this.Resources.Add("buttonGradientBrush", gradientBrush);
            myButton.Background = (Brush)this.Resources["buttonGradientBrush"];
        }
    }
}

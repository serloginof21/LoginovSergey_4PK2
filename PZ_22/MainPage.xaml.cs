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

namespace PZ_22
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ItemChecked(object sender, RoutedEventArgs e)
        {
            double totalPrice = 0;

            foreach (RadioButton coffeeRadioButton in coffeeStackPanel.Children)
            {
                if (coffeeRadioButton.IsChecked == true)
                {
                    double coffeePrice = Convert.ToDouble(coffeeRadioButton.Tag);
                    totalPrice += coffeePrice;
                }
            }

            foreach (RadioButton bakeryRadioButton in bakeryStackPanel.Children)
            {
                if (bakeryRadioButton.IsChecked == true)
                {
                    double bakeryPrice = Convert.ToDouble(bakeryRadioButton.Tag);
                    totalPrice += bakeryPrice;
                }
            }

            foreach (CheckBox additionalPosition in additionalPositionStackPanel.Children)
            {
                if (additionalPosition.IsChecked == true)
                {
                    double additionalPositionPrice = Convert.ToDouble(additionalPosition.Tag);
                    totalPrice += additionalPositionPrice;
                }
            }

            textBlock1.Text = totalPrice.ToString() + " рублей";
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace PZ_6
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        internal class PageInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        private void PageTwoClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page2), "Переход из главного окна на страницу 2");
        }

        private void PageOneClick(object sender, RoutedEventArgs e)
        {
            PageInfo pageInfo = new PageInfo { Id = 001, Name = "Ты попал на страницу №1" };
            Frame.Navigate(typeof(Page1), pageInfo);
        }
    }
}

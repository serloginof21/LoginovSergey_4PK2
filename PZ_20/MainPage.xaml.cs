using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
using NCalc;
using Expression = NCalc.Expression;

namespace PZ_20
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void ButtonNum_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pole.Text = pole.Text + button.Content;
        }

        private void BtnOp_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pole.Text = pole.Text + button.Content;
        }

        private void ravno_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string expression = pole.Text;
                Expression a = new Expression(expression);
                a.EvaluateFunction += (name, args) =>
                {
                    if (name == "pow")
                    {
                        double baseValue = Convert.ToDouble(args.Parameters[0].Evaluate());
                        double exponent = Convert.ToDouble(args.Parameters[1].Evaluate());
                        args.Result = Math.Pow(baseValue, exponent);
                    }
                    else if (name == "sqrt")
                    {
                        double NumberSqr = Convert.ToDouble(args.Parameters[0].Evaluate());
                        args.Result = Math.Sqrt(NumberSqr);
                    }
                };

                object result = a.Evaluate();

                pole.Text = result.ToString();
            }
            catch (Exception ex)
            {
                pole.Text = "Ошибка: " + ex.Message;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorSpeedrunProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool pointed = false;
        int openBracketsNum = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClickPointEvent(object sender, RoutedEventArgs e)
        {
            if (!pointed)
            {
                numLabel.Text += '.';
                pointed = true;
            }
        }

        private void ClickChangeSignEvent(object sender, RoutedEventArgs e)
        {
            if (numLabel.Text[0] == '-')
            {
                numLabel.Text = numLabel.Text.Substring(1);
            }
            else if (numLabel.Text != "0")
            {
                numLabel.Text = '-' + numLabel.Text;
            }
        }

        private void ClickLeftBracketEvent(object sender, RoutedEventArgs e)
        {
            expLabel.Text += '(';
            openBracketsNum++;
        }

        private void ClickRightBracketEvent(object sender, RoutedEventArgs e)
        {
            if (openBracketsNum > 0)
            {
                expLabel.Text += numLabel.Text + ')';
                numLabel.Text = "0";
                openBracketsNum--;
                pointed = false;
            }
        }

        private void ClickBackspaceEvent(object sender, RoutedEventArgs e)
        {
            int len = numLabel.Text.Length;

            if(len == 1 || (len == 2 && numLabel.Text[0] == '-'))
            {
                numLabel.Text = "0";
            }
            else
            {
                if (numLabel.Text[len - 1] == '.')
                {
                    pointed = false;
                }

                numLabel.Text = numLabel.Text.Remove(len - 1);
            }
        }

        private void ClickNumberEvent(object sender, RoutedEventArgs e)
        {
            string key = (sender as Button).Name;
            char num = Constants.charNumEquvs[key.Replace("Button", "")];

            if (numLabel.Text == "0")
            {
                numLabel.Text = num.ToString();
            }
            else
            {
                numLabel.Text += num;
            }
        }  
        
        private void ClickOperationEvent(object sender, RoutedEventArgs e)
        {

        }

        private void KeyUpDelete(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                numLabel.Text = "0";
                expLabel.Text = "";
            }
        }
    }
}

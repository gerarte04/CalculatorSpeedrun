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
        string pressedKey = "-";

        bool pointed = false;
        int openBracketsNum = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private bool MouseUpCheck(object sender)
        {
            if(pressedKey == (sender as Border).Name)
            {
                pressedKey = "-";
                return true;
            }

            pressedKey = "-";
            return false;
        }

        private void MouseDownButtonEvent(object sender, MouseButtonEventArgs e)
        {
            if (pressedKey[0] == '-')
            {
                pressedKey = (sender as Border).Name;
            }
        }

        private void MouseUpPointEvent(object sender, MouseButtonEventArgs e)
        {
            if (!MouseUpCheck(sender))
            {
                return;
            }

            if (!pointed)
            {
                numLabel.Text += '.';
                pointed = true;
            }
        }

        private void MouseUpChangeSignEvent(object sender, MouseButtonEventArgs e)
        {
            if (!MouseUpCheck(sender))
            {
                return;
            }

            if (numLabel.Text[0] == '-')
            {
                numLabel.Text = numLabel.Text.Substring(1);
            }
            else if (numLabel.Text != "0")
            {
                numLabel.Text = '-' + numLabel.Text;
            }
        }

        private void MouseUpLeftBracketEvent(object sender, MouseButtonEventArgs e)
        {
            if (!MouseUpCheck(sender))
            {
                return;
            }

            expLabel.Text += '(';
            openBracketsNum++;
        }

        private void MouseUpRightBracketEvent(object sender, MouseButtonEventArgs e)
        {
            if (!MouseUpCheck(sender))
            {
                return;
            }

            if (openBracketsNum > 0)
            {
                expLabel.Text += numLabel.Text + ')';
                numLabel.Text = "0";
                openBracketsNum--;
                pointed = false;
            }
        }

        private void MouseUpBackspaceEvent(object sender, MouseButtonEventArgs e)
        {
            if (!MouseUpCheck(sender))
            {
                return;
            }

            int len = numLabel.Text.Length;

            if(len == 1)
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

        private void MouseUpNumButtonEvent(object sender, MouseButtonEventArgs e)
        {
            if (!MouseUpCheck(sender))
            {
                return;
            }

            string key = (sender as Border).Name;
            char num = Constants.charNumEquvs[key.Substring(3).Replace("Button", "")];

            if (numLabel.Text == "0")
            {
                numLabel.Text = num.ToString();
            }
            else
            {
                numLabel.Text += num;
            }
        }  
        
        private void MouseUpOperButtonEvent(object sender, MouseButtonEventArgs e)
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

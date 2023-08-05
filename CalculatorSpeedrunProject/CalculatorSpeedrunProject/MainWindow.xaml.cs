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

        Dictionary<string, char> charNumEquvs = new Dictionary<string, char>()
        {
            ["Zero"] = '0',
            ["One"] = '1',
            ["Two"] = '2',
            ["Three"] = '3',
            ["Four"] = '4',
            ["Five"] = '5',
            ["Six"] = '6',
            ["Seven"] = '7',
            ["Eight"] = '8',
            ["Nine"] = '9'
        };

        bool pointed = false;
        int openBracketsNum = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddNumToLabel(char num)
        {
            if (numLabel.Text == "0")
            {
                numLabel.Text = num.ToString();
            }
            else
            {
                numLabel.Text += num;
            }
        }
        private void AddPointToLabel()
        {
            if (!pointed)
            {
                numLabel.Text += '.';
                pointed = true;
            }
        }

        private void ChangeSign()
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

        private void AddLeftBracket()
        {
            expLabel.Text += '(';
            openBracketsNum++;
        }

        private void AddRightBracket()
        {
            if(openBracketsNum > 0)
            {
                expLabel.Text += numLabel.Text + ')';
                numLabel.Text = "0";
                openBracketsNum--;
            }
        }

        private void BackspaceAction()
        {
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

        private void ButtonMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (pressedKey[0] == '-')
            {
                pressedKey = (sender as Border).Name;
            }
        }

        private void ButtonMouseUp(object sender, MouseButtonEventArgs e)
        {
            string key = (sender as Border).Name;

            if (pressedKey == key)
            {
                pressedKey = "-";

                if (key.StartsWith("num"))
                {
                    AddNumToLabel(charNumEquvs[key.Substring(3).Replace("Button", "")]);
                    return;
                }

                switch (key)
                {
                    case "pointButton":
                        AddPointToLabel();
                        return;
                    case "backspaceButton":
                        BackspaceAction();
                        return;
                    case "changeSignButton":
                        ChangeSign();
                        return;
                    case "leftBracketButton":
                        AddLeftBracket();
                        return;
                    case "rightBracketButton":
                        AddRightBracket();
                        return;
                    default:
                        break;
                }



                expLabel.Text += ((sender as Border).Child as TextBlock).Text;
            }

            pressedKey = "-";
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

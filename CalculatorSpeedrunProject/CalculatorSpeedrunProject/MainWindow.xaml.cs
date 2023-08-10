using System;
using System.Collections.Generic;
using System.Globalization;
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
        string lastManageSym = Constants.point;

        LongExp basicExp;

        public MainWindow()
        {
            InitializeComponent();

            basicExp = new LongExp();
        }

        private void ResetNumLabel()
        {
            numLabel.Text = Constants.zero;
            pointed = false;
        }

        private void UpdateExpLabel(string sym)
        {
            string number = numLabel.Text;

            if (number[0] == Constants.minus[0])
            {
                number = Constants.leftBr + number + Constants.rightBr;
            }

            if(lastManageSym == Constants.equal)
            {
                expLabel.Text = "";
            }

            if (lastManageSym != Constants.rightBr)
            {
                expLabel.Text += number;
            }

            expLabel.Text += ' ' + sym + ' ';

            lastManageSym = sym;
        }

        private void ClickPointEvent(object sender, RoutedEventArgs e)
        {
            if (!pointed)
            {
                numLabel.Text += Constants.point;
                pointed = true;
            }
        }

        private void ClickChangeSignEvent(object sender, RoutedEventArgs e)
        {
            if (numLabel.Text[0] == Constants.minus[0])
            {
                numLabel.Text = numLabel.Text.Substring(1);
            }
            else if (numLabel.Text != Constants.zero)
            {
                numLabel.Text = Constants.minus + numLabel.Text;
            }
        }

        private void ClickLeftBracketEvent(object sender, RoutedEventArgs e)
        {
            basicExp.StartNewInnerExp();
            expLabel.Text += Constants.leftBr;
            openBracketsNum++;
            lastManageSym = Constants.leftBr;
        }

        private void ClickRightBracketEvent(object sender, RoutedEventArgs e)
        {
            if (openBracketsNum > 0)
            {
                basicExp.CloseInnerExp(Double.Parse(numLabel.Text, Constants.convertFormat));

                string number = numLabel.Text;

                if (number[0] == Constants.minus[0])
                {
                    number = Constants.leftBr + number + Constants.rightBr;
                }

                if (lastManageSym != Constants.rightBr)
                {
                    expLabel.Text += number;
                }

                expLabel.Text += Constants.rightBr;
                openBracketsNum--;
                ResetNumLabel();

                lastManageSym = Constants.rightBr;
            }
        }

        private void ClickBackspaceEvent(object sender, RoutedEventArgs e)
        {
            int len = numLabel.Text.Length;

            if(len == 1 || (len == 2 && numLabel.Text[0] == Constants.minus[0]))
            {
                numLabel.Text = Constants.zero;
            }
            else
            {
                if (numLabel.Text[len - 1] == Constants.point[0])
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

            if (numLabel.Text == Constants.zero)
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
            string key = (sender as Button).Name;
            OperType op = Constants.enumOperEquivs[key.Replace("Button", "")];

            if (lastManageSym == ")")
            {
                basicExp.AddInnerLongExpOper(op);
            }
            else
            {
                basicExp.AddInnerExpPart(Double.Parse(numLabel.Text, Constants.convertFormat), op);
            }

            UpdateExpLabel((sender as Button).Content.ToString());
            ResetNumLabel();
        }

        private void ClickEnterEvent(object sender, RoutedEventArgs e)
        {
            basicExp.CloseInnerExp(Double.Parse(numLabel.Text, Constants.convertFormat));

            UpdateExpLabel((sender as Button).Content.ToString());
            numLabel.Text = basicExp.Calculate().ToString("0.######", Constants.convertFormat);
            basicExp = new LongExp();
            pointed = false;
            openBracketsNum = 0;
        }

        private void KeyUpDelete(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                basicExp = new LongExp();
                openBracketsNum = 0;
                expLabel.Text = "";
                ResetNumLabel();
            }
        }
    }
}

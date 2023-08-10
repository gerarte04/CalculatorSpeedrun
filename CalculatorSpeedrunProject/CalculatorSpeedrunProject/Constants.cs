using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorSpeedrunProject
{
    public enum OperType
    {
        Add, Sub, Mul, Div
    }

    internal class Constants
    {
        public static Dictionary<string, char> charNumEquvs = new Dictionary<string, char>()
        {
            ["zero"] = '0',
            ["one"] = '1',
            ["two"] = '2',
            ["three"] = '3',
            ["four"] = '4',
            ["five"] = '5',
            ["six"] = '6',
            ["seven"] = '7',
            ["eight"] = '8',
            ["nine"] = '9'
        };

        public static string zero = "0", rightBr = ")", leftBr = "(", point = ".", minus = "–", equal = "=";

        public static Dictionary<string, OperType> enumOperEquivs = new Dictionary<string, OperType>()
        {
            ["add"] = OperType.Add,
            ["sub"] = OperType.Sub,
            ["mul"] = OperType.Mul,
            ["div"] = OperType.Div
        };

        public static NumberFormatInfo convertFormat = new NumberFormatInfo() { 
            NegativeSign = Constants.minus,
            NumberDecimalSeparator = Constants.point,
        };

        public static bool IsPrimaryOper(OperType op)
        {
            return op == OperType.Mul || op == OperType.Div;
        }

        public static double ApplyOper(OperType op, double n1, double n2)
        {
            switch (op)
            {
                case OperType.Add:
                    return n1 + n2;
                case OperType.Sub:
                    return n1 - n2;
                case OperType.Mul:
                    return n1 * n2;
                case OperType.Div:
                    return n1 / n2;
                default:
                    return 0;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorSpeedrunProject
{
    public class NumExp : Exp
    {
        double number;

        public NumExp(double num)
        {
            number = num;
        }

        public override double Calculate()
        {
            return number;
        }
    }
}

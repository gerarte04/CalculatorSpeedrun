using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorSpeedrunProject
{
    public class LongExp : Exp
    {
        List<Exp> exps;
        List<OperType> opers;
        public bool Closed { get; private set; } = false;

        public LongExp()
        {
            exps = new List<Exp>();
            opers = new List<OperType>();
        }

        public void AddInnerExpPart(double num, OperType oper)
        {
            var last = exps.LastOrDefault();

            if (last == null || last is NumExp || (last as LongExp).Closed)
            {
                exps.Add(new NumExp(num));
                opers.Add(oper);
            }
            else
            {
                (last as LongExp).AddInnerExpPart(num, oper);
            }
        }

        public void AddInnerLongExpOper(OperType oper)
        {
            var last = exps.LastOrDefault();

            if (last == null || last is NumExp || (last as LongExp).Closed)
            {
                opers.Add(oper);
            }
            else
            {
                (last as LongExp).AddInnerLongExpOper(oper);
            }
        }

        public void StartNewInnerExp()
        {
            var last = exps.LastOrDefault();

            if (last == null || last is NumExp || (last as LongExp).Closed)
            {
                exps.Add(new LongExp());
            }
            else
            {
                (last as LongExp).StartNewInnerExp();
            }
        }

        public void CloseInnerExp(double lastNum)
        {
            var last = exps.LastOrDefault();

            if (last == null || last is NumExp || (last as LongExp).Closed)
            {
                exps.Add(new NumExp(lastNum));
                Closed = true;
            }
            else
            {
                (last as LongExp).CloseInnerExp(lastNum);
            }
        }

        public override double Calculate()
        {
            List<double> calcs = new List<double>();
            List<OperType> tempOpers = new List<OperType>(opers);

            foreach(Exp e in exps)
            {
                calcs.Add(e.Calculate());
            }

            int i = 0;
            int len = tempOpers.Count();

            while (i < len) { 
                if (Constants.IsPrimaryOper(tempOpers[i]))
                {
                    calcs[i] = Constants.ApplyOper(tempOpers[i], calcs[i], calcs[i + 1]);
                    calcs.RemoveAt(i + 1);
                    tempOpers.RemoveAt(i);
                    len--;
                }
                else
                {
                    i++;
                }
            }

            double res = calcs[0];

            for(i = 0; i < len; i++)
            {
                res = Constants.ApplyOper(tempOpers[i], res, calcs[i + 1]);
            }

            return res;
        }
    }
}

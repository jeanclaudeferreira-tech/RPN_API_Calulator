using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpnCalculator_V1
{
    public class RPNCalc
    {
        private Stack<double> _operands { get; set; } = new ();
        public RPNCalc() { }

        public double Calc(string rpnString)
        {
            if (String.IsNullOrEmpty(rpnString))
                throw new ArgumentException("Input should not be empty");

            var inputStrings = rpnString.Split(" ");
            if (inputStrings.Length < 3) throw new ArgumentException("Input must contain at least 3 items");
            for (int i=0; i < inputStrings.Length; i++)
            {
                var stringToProcess = inputStrings[i].Trim();
                if (int.TryParse(stringToProcess, out int val))
                {
                    _operands.Push(val);
                }
                else if (stringToProcess.Equals("+"))
                {
                    ProcessOperator(EnumOperating.Add);
                }
                else if (stringToProcess.Equals("-"))
                {
                    ProcessOperator(EnumOperating.Substract);
                }
                else if (stringToProcess.Equals("*"))
                {
                    ProcessOperator(EnumOperating.Multiplication);
                }
                else if (stringToProcess.Equals("/"))
                {
                    ProcessOperator(EnumOperating.Division);
                }
                else
                    throw new ArgumentException($"Invalid character on sequence {i}");

            }
            if(_operands.Count != 1)
                throw new ArgumentException("The operation is inconsistent");
            return _operands.Pop();
        }

        private double ProcessOperator(EnumOperating ope)
        {
            if (_operands.Count < 2)
                throw new ArgumentException("There are not enough operands");
            var val2 = _operands.Pop();
            var val1 = _operands.Pop();
            switch(ope)
            {
                case EnumOperating.Add:
                    _operands.Push(val1 + val2);
                    break;
                case EnumOperating.Substract:
                    _operands.Push(val1 - val2);
                    break;
                case EnumOperating.Multiplication:
                    _operands.Push(val1 * val2);
                    break;
                case EnumOperating.Division:
                    //exception if divide by zero
                    _operands.Push(val1 / val2);
                    break;
            }
            return 0.0;
        }


    }
}

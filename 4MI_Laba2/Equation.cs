using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace _4MI_Laba2
{
    public class Equation
    {
        public double[] coefficients;
        public double[] variables;
        public double constantTerm;

        public Equation(double[] _coefficients, double[] _variables, double _constantTerm)
        {
            coefficients = _coefficients;
            variables = _variables;
            constantTerm = _constantTerm;
        }

        public double Solve(int indexOfCurrVariable)
        {
            double res = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                if (i == indexOfCurrVariable) {
                    res += constantTerm / coefficients[i];
                    continue;
                }
                res += (-1f * coefficients[i]) * variables[i] / coefficients[indexOfCurrVariable];
            }
            return res;
        }
        
        public override string ToString()
        {
            List<string> res = new List<string>();
            for (int i = 0; i < variables.Length; i++)
            {
                // :)
                if (i != 0) res.Add($"{(coefficients[i] < 0 ? "-" : "+")} ");
                res.Add($"{(coefficients[i] < 0 ? $"{(-1f * coefficients[i]):0.####} * " : $"{coefficients[i]:0.####} * ")}" +
                        $"{(variables[i] == null ? $"x{i}" : $"{variables[i]:0.####}")} ");
            }
            res.Add($"= {constantTerm}");
            return string.Join("", res);
        }
    }
}
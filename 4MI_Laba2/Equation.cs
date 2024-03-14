using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace _4MI_Laba2
{
    public class Equation
    {
        public Vector<double> coefficients;
        public Vector<double> variables;
        public double constantTerm;

        public Equation(double[] _coefficients, double[] _variables, double _constantTerm)
        {
            coefficients = Vector<double>.Build.Dense(_coefficients);
            variables = Vector<double>.Build.Dense(_variables);
            constantTerm = _constantTerm;
        }
        public Equation(Vector<double> _coefficients, Vector<double> _variables, double _constantTerm)
        {
            coefficients = _coefficients.Clone();
            variables = _variables.Clone();
            constantTerm = _constantTerm;
        }

        public double Solve(int indexOfCurrVariable)
        {
            double res = 0;
            for (int i = 0; i < coefficients.Count; i++)
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
            for (int i = 0; i < variables.Count; i++)
            {
                if (i != 0) res.Add($"{(coefficients[i] < 0 ? "-" : "+")} ");
                res.Add($"{(coefficients[i] < 0 ? $"{(-1f * coefficients[i]):0.####} * " : $"{coefficients[i]:0.####} * ")}" +
                        $"{(variables[i] == null ? $"x{i}" : $"{variables[i]:0.####}")} ");
            }
            res.Add($"= {constantTerm}");
            return string.Join("", res);
        }
    }
}
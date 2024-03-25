using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;    

namespace _4MI_Laba2
{
    public class Equation
    {
        private Vector<double> coefficients;
        private Vector<double> variables;
        private double constantTerm;
        public bool varsAreNull = true;

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

        public double Evalv()
        {
            double res = 0;
            for (int i = 0; i < coefficients.Count; i++)
                res += coefficients[i] * variables[i];

            return res;
        }
        
        public string ToString(bool skipConst)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < variables.Count; i++)
            {
                if (i != 0) res.Add($"{(coefficients[i] < 0 ? "-" : "+")} ");
                if (i == 0) { 
                    res.Add($"{(coefficients[i] < 0 ? $"{(coefficients[i]):0.####} * " : $" {coefficients[i]:0.####} * ")}" +
                                   $"{(varsAreNull ? $"x{i}" : $"{variables[i]:0.####}")} ");
                    continue;
                }
                res.Add($"{(coefficients[i] < 0 ? $"{(-1f * coefficients[i]):0.####} * " : $"{coefficients[i]:0.####} * ")}" +
                        $"{(varsAreNull ? $"x{i}" : $"{variables[i]:0.####}")} ");
            }
            if (!skipConst) res.Add($"= {constantTerm}");
            return string.Join("", res);
        }
    }
}
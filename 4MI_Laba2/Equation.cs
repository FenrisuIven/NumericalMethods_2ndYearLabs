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
        public double constTerm;

        public Equation(double[] coef, double[] vars, double cons)
        {
            coefficients = coef;
            variables = vars;
            constTerm = cons;
        }

        public double GetRes(int idxVar)
        {
            double res = 0;
            int count = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                if (i == idxVar - 1) {
                    res += constTerm / coefficients[i];
                    continue;
                }
                res += (coefficients[i] * -1f) * variables[i] / coefficients[idxVar - 1];
            }
            return res;
        }
        
        public override string ToString()
        {
            List<string> res = new List<string>();
            double prev = 0;
            for (int i = 0; i < variables.Length; i++)
            {
                if (i != 0) res.Add($"{(coefficients[i] < 0 ? "-" : "+")} ");
                res.Add($"{(coefficients[i] == null ? "" : $"{coefficients[i]}*")}{(variables[i] == 0 ? $"x{i}" : $"{variables[i]}")} ");
            }
            res.Add($"= {constTerm}");
            return string.Join("", res);
        }
    }
}
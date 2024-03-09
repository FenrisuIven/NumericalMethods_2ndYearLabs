using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace _4MI_Laba2
{
    public class SLE
    {
        public double[]  variables;
        public double[,] coefficients;
        public double[]  constantTerms;

        public SLE(double[] vars, double[,] coeffs, double[] term)
        {
            variables = vars;
            coefficients = coeffs;
            constantTerms = term;
        }

        public Equation GetRowEq(int idxRow)
        {
            double[] currentRowCoefficients = new double[coefficients.GetLength(1)];
            for (int i = 0; i < coefficients.GetLength(1); i++)
                currentRowCoefficients[i] = coefficients[idxRow, i];
            //contemplating whether or not I should switch to jagged at this point...
            
            return new Equation(currentRowCoefficients, variables, constantTerms[idxRow]);
        }
        
        public override string ToString()
        {
            List<string> res = new List<string>();
            for (int i = 0; i < coefficients.GetLength(0); i++)
            {
                if (i != coefficients.GetLength(0) - 1) res.Add(GetRowEq(i).ToString() + "\n");
                else res.Add(GetRowEq(i).ToString());
            }
            
            return string.Join("", res);
        }
    }
}
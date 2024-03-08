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
        public double[]  vector_X;
        public double[,] matrix_A;
        public double[]  vector_B;

        public SLE(double[] x, double[,] a, double[] b)
        {
            vector_X = x;
            matrix_A = a;
            vector_B = b;
        }

        public Equation GetRowEq(int idxRow)
        {
            double[] coeffs = new double[matrix_A.GetLength(1)];
            for (int i = 0; i < matrix_A.GetLength(1); i++)
            {
                coeffs[i] = matrix_A[idxRow, i];
            }
            return new Equation(coeffs, vector_X, vector_B[idxRow]);
        }
        
        public override string ToString()
        {
            List<string> res = new List<string>();
            for (int i = 0; i < matrix_A.GetLength(0); i++)
            {
                for (int j = 0; j < matrix_A.GetLength(1); j++)
                {
                    res.Add($"{matrix_A[i, j]}*{vector_X[j]} ");
                }
                res.Add($"= {vector_B[i]}\n");
            }

            return string.Join("", res);
        }
    }
}
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
    public class SLE
    {
        public Vector<double> variables;
        public Matrix<double> coefficients;
        public Vector<double> constantTerms;

        public SLE(double[] _variables, double[,] _coefficients, double[] term)
        {
            variables = DenseVector.OfArray(_variables);
            coefficients = DenseMatrix.OfArray(_coefficients);
            constantTerms = DenseVector.OfArray(term);
        }
        public SLE(Vector<double> _variables, Matrix<double> _coefficients, Vector<double> _constantTerms)
        {
            variables = _variables.Clone();
            coefficients = _coefficients.Clone();
            constantTerms = _constantTerms.Clone();
        }

        private Equation GetRowEq(int idxRow) => 
            new Equation(coefficients.Row(idxRow), variables, constantTerms[idxRow]);
        
        public override string ToString()
        {
            List<string> res = new List<string>();
            for (int i = 0; i < coefficients.RowCount; i++)
            {
                if (i != coefficients.RowCount - 1) res.Add(GetRowEq(i).ToString() + "\n");
                else res.Add(GetRowEq(i).ToString());
            }
            
            return string.Join("", res);
        }
        
        #region Normalisation

        public void Normalise()
        {
            var coefficients_Transposed = coefficients.Transpose();
            coefficients *= coefficients_Transposed;
            constantTerms *= coefficients_Transposed;
        }
        #endregion
    }
}
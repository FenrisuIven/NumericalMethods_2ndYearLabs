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
        public bool varsAreNull = true;
        
        public SLE(double[] _variables, double[,] _coefficients, double[] term)
        {
            variables = DenseVector.OfArray(_variables);
            coefficients = DenseMatrix.OfArray(_coefficients);
            constantTerms = DenseVector.OfArray(term);
        }
        public SLE(Vector<double> _variables, Matrix<double> _coefficients, Vector<double> _constantTerms)
        {
            if (_variables.Count != _coefficients.ColumnCount) throw new ArgumentException();
            else if (_constantTerms.Count != _coefficients.RowCount) throw new ArgumentException();
            
            variables = _variables.Clone();
            coefficients = _coefficients.Clone();
            constantTerms = _constantTerms.Clone();
        }

        public Equation GetRowEq(int idxRow) => 
            new Equation(coefficients.Row(idxRow), variables, constantTerms[idxRow]);
        
        public string ToString(bool skipConst)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < coefficients.RowCount; i++)
            {
                var temp = GetRowEq(i);
                temp.varsAreNull = varsAreNull;
                if (i != coefficients.RowCount - 1) res.Add(temp.ToString(skipConst) + "\n");
                else res.Add(temp.ToString(skipConst));
            }
            
            return string.Join("", res);
        }
        
        #region Normalisation

        public void Normalise()
        {
            Color.WriteLine(2,"- Виконується нормалізація матриці...");
            var coefficients_Transposed = coefficients.Transpose();
            coefficients = coefficients_Transposed * coefficients;
            constantTerms = coefficients_Transposed * constantTerms;
        }
        #endregion
    }
}
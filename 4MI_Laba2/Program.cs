using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace _4MI_Laba2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int amountOfVariables = 3;
            double accuracy = 0.001;
            
            Vector<double> variables = Vector<double>.Build.Dense(amountOfVariables, 1);
            Matrix<double> coefficients = DenseMatrix.OfArray(
                new double[,] {
                    {4, 1, 2},
                    {5, 2.2, 1.5},
                    {12, 3.8, 14} });
            Vector<double> constantTerms = DenseVector.OfArray(
                new double[] { 3, 11, 5 });
            
            Vector<double> approximations = Vector<double>.Build.Dense(3, 1);
            
            
            SLE firstTest = new SLE(variables, coefficients, constantTerms);
            Console.WriteLine("\nПочаткова СЛАР:\n" + firstTest);

            SimpleIterations si = new SimpleIterations(approximations, accuracy, amountOfVariables);
            firstTest.variables = si.Start( firstTest.coefficients, firstTest.constantTerms);
            
            Console.WriteLine(firstTest);
            //correct answer is: [ -21.75 ; 20.083 ; 8.4167 ]
            //outputs: [ -21.7494 ; -20.0829 ; -8.1466 ]
        }
    }
}
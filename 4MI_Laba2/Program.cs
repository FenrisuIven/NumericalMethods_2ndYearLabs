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
            double accuracy = 0.01;
            
            #region Вар 1
            /*Vector<double> variables = Vector<double>.Build.Dense(amountOfVariables, 1);
            Matrix<double> coefficients = DenseMatrix.OfArray(
                new double[,] {
                    {4, 1, 2},
                    {5, 2.2, 1.5},
                    {12, 3.8, 14}});
            Vector<double> constantTerms = DenseVector.OfArray(
                new double[] { 3, 11, 5 });*/
            #endregion
            
            #region Вар 9
            Vector<double> variables = Vector<double>.Build.Dense(amountOfVariables, 1);
            Matrix<double> coefficients = DenseMatrix.OfArray(
                new double[,] {
                    {-2,  1, 1},
                    {1,  -2, 1},
                    {-1, 3, -6}});
            Vector<double> constantTerms = DenseVector.OfArray(
                new double[] { 15, 10, 12 });
            #endregion
            
            #region З прикладу розв'язання
            /*Vector<double> variables = Vector<double>.Build.Dense(amountOfVariables, 1);
            Matrix<double> coefficients = DenseMatrix.OfArray(
                new double[,] {
                    {9, -2, 5},
                    {5, 20, 12},
                    {6, 7, 45}});
            Vector<double> constantTerms = DenseVector.OfArray(
                new double[] { 8, 44, 97 });*/
            #endregion
            
            Vector<double> approximations = Vector<double>.Build.Dense(amountOfVariables, 1);
            bool enableOutput = false;
            bool normaliseMatrix = false;
            
            SLE firstTest = new SLE(variables, coefficients, constantTerms);
            Console.WriteLine("\nПочаткова СЛАР:\n" + firstTest.ToString(false));

            Console.WriteLine($"Налаштування:" +
                              $"\n\tenableOutput:\t\t {enableOutput}" +
                              $"\n\tnormaliseMatrix:\t {normaliseMatrix}");
            
            SimpleIterations si = new SimpleIterations(approximations, accuracy, amountOfVariables, enableOutput, normaliseMatrix);
            si.Start(firstTest);

            firstTest.varsAreNull = false;
            foreach (double val in firstTest.variables)
            {
                Console.WriteLine("x: {0:0.00000000}", val);
            }
            
            Console.WriteLine("\nФінальна СЛАР:");
            for (int i = 0; i < amountOfVariables; i++)
            {
                var row = firstTest.GetRowEq(i);
                row.varsAreNull = false;
                Console.WriteLine(row.ToString(true) + " = " + $"{(row.Evalv()):0.0000}");
            }
            //correct answer is: [ -21.75 ; 20.083 ; 8.4167 ]
            //outputs: [ -21.7494 ; -20.0829 ; -8.1466 ]
        }
    }
}
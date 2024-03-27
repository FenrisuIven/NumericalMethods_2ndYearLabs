using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;   

// Вар 7. Метод Ньютона
// 4х + 11у^2 = 0
// 11x + 7y^3 + 33 = 0

namespace _4MI_Laba3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var eq = new Equation();
            var elements = new List<Expression<Func<double, double>>>();
            Expression<Func<double, double>> expr = x => 4 * x;
            elements.Add(expr);
            expr = y => 11 * Math.Pow(y, 2);
            elements.Add(expr);

            List<string> operations = new List<string>();
            operations.Add("+");

            double constantTerm = 0; 
            
            eq.Initialize(elements, operations, constantTerm);
            
            Console.WriteLine(eq.FormEquation.BasicString(true));
            Console.WriteLine(eq.FormEquation.EvalvedString(3, 3));
        }
    }
}
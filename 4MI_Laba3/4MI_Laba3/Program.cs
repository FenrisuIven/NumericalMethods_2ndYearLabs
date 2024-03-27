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
        public static NonlinearSystem system;

        private static void FormSystem()
        {
            #region First Equation
            var firstEq = new Equation();
            var firstElems = new List<Expression<Func<double, double>>>
            {
                x => 4 * x,
                y => 11 * Math.Pow(y, 2)
            };
            List<string> firstOps = new List<string> { "+", "+" };
            double firstConst = 0; 
            #endregion
            firstEq.Initialize(firstElems, firstOps, firstConst);
            
            #region Second Equation
            var secondEq = new Equation();
            var secondElems = new List<Expression<Func<double, double>>>
            {
                x => 11 * x,
                y => 7 * Math.Pow(y, 3),
                c => 33
            };
            List<string> secondOps = new List<string> { "+", "+" };
            double secondConst = 0; 
            #endregion
            secondEq.Initialize(secondElems, secondOps, secondConst);

            system = new NonlinearSystem(new List<Equation> { firstEq, secondEq });
        }
        
        public static void Main(string[] args)
        {
            FormSystem();
            Console.WriteLine(system.ToString());

            NewtonsMethod method = new NewtonsMethod();
            method.Initialize(new double[] { -4, 1 }, system);
            method.CheckConvergence();
        }
    }
}
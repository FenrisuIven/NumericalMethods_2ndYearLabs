using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace _4MI_Laba2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double[] testVectorX = new double[] { 1, 2, 3 };
            double[,] testMatrixA = new double[,]
            {
                {4, 4, 4},
                {5, 5, 5},
                {6, 6, 6}
            };
            double[] testVectorB = new double[] { 7, 8, 9 };
            
            SLE sle = new SLE(testVectorX, testMatrixA, testVectorB);
            Console.WriteLine(sle);

            
            //9x1 - 2x2 + 5x3 = 8
            Equation eq = new Equation(new double[]{9, -2, 5},
                new double[]{1, 1, 1}, 8);
            //9x1 = 8 + 2x2 - 5x3
            //x1 = 8/9 + 2/9 x2 + 5/9 x3
            Console.WriteLine(eq.GetRes(1));
            //x1 = 8/9 + 2/9 + 5/9 = 0.55

            //5x1 + 20x2 + 12x3 = 44
            eq = new Equation(new double[] {5, 20, 12}, 
                new double[]{0.55, 1, 1}, 44);
            //x2 = 44/20 - 5/20 x1 - 12/20 x3
            //x2 = 2.2 - 0.25 - 0.6 = 1.46
            Console.WriteLine(eq.GetRes(2));
        }
    }
}
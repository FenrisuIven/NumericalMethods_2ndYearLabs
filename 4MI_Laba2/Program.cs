using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace _4MI_Laba2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double[] testVectorA = new double[] { 1, 2, 3 };
            double[,] testMatrixX = new double[,]
            {
                {4, 4, 4},
                {5, 5, 5},
                {6, 6, 6}
            };
            double[] testVectorB = new double[] { 7, 8, 9 };
            
            SLE sle = new SLE(testVectorA, testMatrixX, testVectorB);
            Console.WriteLine(sle);
        }
    }
}
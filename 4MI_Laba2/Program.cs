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
            SLE firstTest = new SLE(
                new double[] { 1, 1, 1 }, 
                new double[,] {
                    { -2, 1, 1 },
                    {1, -2, 1},
                    {-1, 3, -6} }, 
                new double[]{15, 10, 12}
            );
            Console.WriteLine("\nПочаткова СЛАР:\n" + firstTest);

            SimpleIterations si = new SimpleIterations(new double[]{1,1,1}, 0.001f, 3);
            firstTest.variables = si.Start(firstTest.coefficients, firstTest.constantTerms);
            
            Console.WriteLine(firstTest);
            //correct answer is: [ -21.75 ; 20.083 ; 8.4167 ]
            //outputs: [ -21.7494 ; -20.0829 ; -8.1466 ]
        }
    }
}
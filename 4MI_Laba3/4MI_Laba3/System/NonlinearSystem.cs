using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using _4MI_Laba3.Calculator;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double; 

namespace _4MI_Laba3
{
    public class NonlinearSystem
    {
        private List<Equation> _system = new List<Equation>();
        private Dictionary<string, double> _variables = new Dictionary<string, double>();
        public NonlinearSystem(List<Equation> system)
        {
            foreach (var equation in system) _system.Add(equation);
        }

        public Equation GetEquationAt(int idx) => _system.ElementAt(idx);
        
        public string ToString()
        {
            List<string> res = new List<string>();
            foreach (var eq in _system) Console.WriteLine(eq.FormEquation.BasicString(true));
            return string.Join("", res);
        }
    }
}
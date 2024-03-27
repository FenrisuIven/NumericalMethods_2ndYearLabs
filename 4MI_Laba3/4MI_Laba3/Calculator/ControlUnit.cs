using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;  

namespace _4MI_Laba3.Calculator
{
    public class ControlUnit
    {
        private ArithmeticUnit unit = new ArithmeticUnit();

        public double Exec(string operation, double operand)
        {
            unit.Run(operation, operand);
            return unit.Register;
        }
        public double GetResult => unit.Register;
    }
}
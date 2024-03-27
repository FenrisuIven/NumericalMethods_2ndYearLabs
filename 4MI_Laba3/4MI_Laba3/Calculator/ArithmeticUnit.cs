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
    public class ArithmeticUnit
    {
        public double Register { get; private set; }

        public void Run(string operation, double operand)
        {
            switch (operation)
            {
                case "+":
                    Register += operand;
                    break;
                case "-":
                    Register -= operand;
                    break;
            }
        }
    }
}
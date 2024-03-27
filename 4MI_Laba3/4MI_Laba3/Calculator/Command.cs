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
    public abstract class Command
    {
        protected ArithmeticUnit _unit;
        protected double _operand;
        public abstract void Exec();
    }

    class Add : Command
    {
        public Add(ArithmeticUnit unit, double operand)
        {
            _unit = unit;
            _operand = operand;
        }

        public override void Exec() => _unit.Run("+", _operand);
    }
    
    class Sub : Command
    {
        public Sub(ArithmeticUnit unit, double operand)
        {
            _unit = unit;
            _operand = operand;
        }

        public override void Exec() => _unit.Run("-", _operand);
    }
}
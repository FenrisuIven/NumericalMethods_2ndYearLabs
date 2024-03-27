using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using _4MI_Laba3.Calculator;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double; 

// Вар 7. Метод Ньютона
// 4х + 11у^2 = 0
// 11x + 7y^3 + 33 = 0

namespace _4MI_Laba3
{
    public class NewtonsMethod
    {
        private Dictionary<string, double> pointCoord = new Dictionary<string, double>();
        private NonlinearSystem _system;
        
        public void Initialize(double[] _startingPoint, NonlinearSystem system)
        {
            _system = system;
            
            //I know :)
            pointCoord.Add("x", _startingPoint[0]);
            
            if (1 > _startingPoint.Length - 1) return;
            pointCoord.Add("y", _startingPoint[1]);
            
            if (2 > _startingPoint.Length - 1) return;
            pointCoord.Add("x", _startingPoint[2]);
        }
        
        
        #region Check Convergence
        
        public void CheckConvergence()
        {
            //I know :)
            double dF1 = Derived_Top_TwoVars(_system.GetEquationAt(0), pointCoord["x"] + 0.01, pointCoord["y"], 0.01);
            double dF1dX = Derived(dF1, 0.01);
            
            double dF2 = Derived_Top_TwoVars(_system.GetEquationAt(1), pointCoord["x"] + 0.01, pointCoord["y"], 0.01);
            double dF2dX = Derived(dF2, 0.01);
            
            dF1 = Derived_Top_TwoVars(_system.GetEquationAt(0), pointCoord["x"], pointCoord["y"] + 0.01, 0.01);
            double dF1dY = Derived(dF1, 0.01);
            
            dF2 = Derived_Top_TwoVars(_system.GetEquationAt(1), pointCoord["x"], pointCoord["y"] + 0.01, 0.01);
            double dF2dY = Derived(dF2, 0.01);

            Console.WriteLine($"dF1/dX = {dF1dX}\ndF1/dY = {dF1dY}\ndF2/dX = {dF2dX}\ndF/dY = {dF2dY}");
        }

        public double Derived_Top_TwoVars(Equation F, double X, double Y, double increase) => 
            F.Evaluate(X, Y) - F.Evaluate(pointCoord["x"], pointCoord["y"]);
        
        public double Derived(double topPart, double increase) => 
            topPart / increase;
            
        #endregion
        
        
        public void Start(double[] _startingPoint)
        {
            //tbd
        }
    }
}
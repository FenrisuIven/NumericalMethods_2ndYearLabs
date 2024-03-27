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
    public class Equation
    {
        private List<Expression<Func<double, double>>> _elements = new List<Expression<Func<double, double>>>();
        private List<string> _operations = new List<string>();
        private double _constantTerm;

        public Formatter FormEquation;
        
        public void Initialize(List<Expression<Func<double, double>>> elements, List<string> operations, double constantTerm)
        {
            foreach (var eq in elements) _elements.Add(eq);
            foreach (var op in operations) _operations.Add(op);
            _constantTerm = constantTerm;

            FormEquation = new Formatter(this);
        }

        public double ConstTerm
        {
            get { return _constantTerm; }
            set { _constantTerm = value; }
        }

        public Expression<Func<double, double>> GetElementAt(int idx) => _elements.ElementAt(idx);
        public void AddElement(Expression<Func<double, double>> elem) => _elements.Add(elem);
        public int GetElementCount() => _elements.Count;
        public double GetElementRes(int idx, double val) => (GetElementAt(idx).Compile())(val);
        
        public string GetOperationAt(int idx) => _operations.ElementAt(idx);
        public void AddOperation(string op) => _operations.Add(op);

        public double Evaluate(double x, double y)
        {
            ControlUnit Calculator = new ControlUnit();
            
            double firstVariable = GetElementRes(0, GetElementAt(0).ToString().Contains("x") ? x : y);
            Calculator.Exec("+", firstVariable);
            
            for (int i = 1; i < GetElementCount(); i++)
            {
                var bodyOfElem = GetElementAt(i).Body;
                double evalvedElem = GetElementRes(i, (bodyOfElem.ToString().Contains("x") ? x : y));
                
                string operation = GetOperationAt(i - 1);
                Calculator.Exec(operation, evalvedElem);
            };
            
            return Calculator.GetResult;
        }
        
    }

    public class Formatter
    {
        private Equation _eq;
        public Formatter(Equation eq) => _eq = eq;
        
        public string BasicString(bool withConst)
        {
            List<string> res = new List<string>
            {
                $"{_eq.GetElementAt(0).Body}"
            };
            
            for (int i = 1; i < _eq.GetElementCount(); i++)
                res.Add($" {_eq.GetOperationAt(i - 1)} {_eq.GetElementAt(i).Body}");

            if (withConst) res.Add($" = {_eq.ConstTerm}");
            
            return string.Join("", res);
        }

        public string EvalvedString(double x, double y)
        {
            List<string> stringRes = new List<string>();
            for (int i = 0; i < _eq.GetElementCount(); i++)
            {
                var bodyOfElem = _eq.GetElementAt(i).Body;
                double evalvedElem = _eq.GetElementRes(i, (bodyOfElem.ToString().Contains("x") ? x : y));
                
                stringRes.Add($"{evalvedElem}");
                
                if (i < _eq.GetElementCount() - 1) stringRes.Add($" {_eq.GetOperationAt(i)} ");
            }

            double doubleRes = _eq.Evaluate(x, y);
            stringRes.Add($" = {doubleRes}");
            
            return string.Join("", stringRes);
        }
    }
}
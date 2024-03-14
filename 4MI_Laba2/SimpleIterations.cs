using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace _4MI_Laba2
{
    public class SimpleIterations
    {
        private SLE sle;
        
        private Vector<double> initialApproximation;
        private Vector<double> foundApproximations;
        
        private double accuracy;
        private int amountOfVariables;
        
        private Vector<double> prevAbsoluteDifference;
        private Vector<double> currentAbsoluteDifferences;

        private int countOfIncreasedAccDifference = 0;
        private bool firstAccDiffComparison = true;
        
        public bool matrixNeedsToBeNormalised = false;
        public bool matrixIsNormalised = false;

        public SimpleIterations(SLE _sle)
        {
            sle = _sle;
        }
        public SimpleIterations(Vector<double> _approximations, double _accuracy, int _amountOfVariables)
        {
            initialApproximation = Vector<double>.Build.Dense(_approximations.Count);
            _approximations.CopyTo(initialApproximation);
            
            amountOfVariables = _amountOfVariables;
            accuracy = _accuracy;

            foundApproximations = Vector<double>.Build.Dense(_approximations.Count);
            initialApproximation.CopyTo(foundApproximations);
            
            prevAbsoluteDifference = Vector<double>.Build.Dense(amountOfVariables);
            currentAbsoluteDifferences = Vector<double>.Build.Dense(amountOfVariables);
        }
        
        public Vector<double> Start(Matrix<double> coefficients, Vector<double> constantTerms)
        {
            int i = 0;
            while(true)  
            {
                DisplayIterationNum(i);
                for (int j = 0; j < amountOfVariables; j++)  
                {
                    Equation currentRowToEquation = new Equation(
                        coefficients.Row(j), foundApproximations, constantTerms[j]);
                    double currentEquationResult = currentRowToEquation.Solve(j);
                    foundApproximations[j] = currentEquationResult;
                }

                if (CurrentSolutionIsAcceptable())
                {
                    Console.WriteLine("\nFound solution"); 
                    return foundApproximations;
                }
                
                if (countOfIncreasedAccDifference == 3)
                {
                    if (!matrixIsNormalised)
                    {
                        matrixNeedsToBeNormalised = true;
                        Console.WriteLine("\nMatrix needs to be normalised");
                        //matrixIsNormalised;
                    }
                    else throw new ArgumentException("Something is wrong with the matrix");
                }
                
                foundApproximations.CopyTo(initialApproximation);
                i++;
            }
        }

        private bool CurrentSolutionIsAcceptable()
        {
            CalculateDifferences();
            bool isAcceptable = true;
            for (int i = 0; i < amountOfVariables; i++)
            {   
                #region . "Порівняння знайденого розв'язку з попереднім" .
                Console.WriteLine($"| {foundApproximations[i]:0.00000} - " +
                                  $"{initialApproximation[i]:0.00000} | = " +
                                  $"{currentAbsoluteDifferences[i]:0.00000}" +
                                  $"{(currentAbsoluteDifferences[i] < accuracy ? " < ":" > ")}" +
                                  $"{accuracy:0.00000}");
                #endregion
                if (currentAbsoluteDifferences[i] > accuracy && isAcceptable) 
                    isAcceptable = false;
            }

            if (!isAcceptable)
            {
                if (firstAccDiffComparison) firstAccDiffComparison = false;
                else 
                {
                    for (int i = 0; i < amountOfVariables; i++)
                    {
                        if (Math.Abs(currentAbsoluteDifferences[i]) > Math.Abs(prevAbsoluteDifference[i]))
                        {
                            Console.WriteLine(" - ! Одна з різниць збільшилася у порівнянні з минулим разом");
                            countOfIncreasedAccDifference++;
                            break;
                        }
                    }
                }
                for (int i = 0; i < amountOfVariables; i++)
                    prevAbsoluteDifference[i] = currentAbsoluteDifferences[i];
            }
            return isAcceptable;
        }

        private void CalculateDifferences()
        {
            Func<int, double> absoluteDifference = (idx) => 
                Math.Abs(foundApproximations[idx] - initialApproximation[idx]);
            
            for (int i = 0; i < currentAbsoluteDifferences.Count; i++)
                currentAbsoluteDifferences[i] = absoluteDifference(i);
        }
        
        private void DisplayIterationNum(int idx)
        {
            Console.Write("\nІтерація №{0}. Початкове наближення: [ ", idx);
            for (int i = 0; i < amountOfVariables; i++)
            {
                if (i != amountOfVariables - 1) Console.Write($"{initialApproximation[i]:0.0000} ; ");
                else Console.Write($"{initialApproximation[i]:0.0000} ");
            }
            Console.Write("]\n");
        }
    }
}
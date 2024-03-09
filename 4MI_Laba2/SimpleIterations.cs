using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace _4MI_Laba2
{
    //I will be adding output of in-between steps of calculation,
    //matrix transposition and SLE normalisation algorithms
    //and a lot more...
    
    //As well as fixing current issues with- ahem- the amount of loops here. Whoops.
    public class SimpleIterations
    {
        public double[] initialApproximation;
        private double[] foundApproximations;
        
        public double accuracy;
        public int amountOfVariables;
        
        private double[] prevAbsoluteDifference;
        private double[] currentAbsoluteDifferences;

        private int countOfIncreasedAccDifference = 0;
        private bool firstAccDiffComparison = true;
        
        public bool matrixNeedsToBeNormalised = false;
        public bool matrixIsNormalised = false;
        
        public SimpleIterations(double[] _approximations, double _accuracy, int _amountOfVariables)
        {
            initialApproximation = _approximations;
            amountOfVariables = _amountOfVariables;
            accuracy = _accuracy;

            foundApproximations = new double[amountOfVariables];
            initialApproximation.CopyTo(foundApproximations, 0);
            
            prevAbsoluteDifference = new double[amountOfVariables];
            currentAbsoluteDifferences = new double[amountOfVariables];
        }
        
        public double[] Start(double[,] coefficients, double[] constantTerms)
        {
            int i = 0;
            while(true)  
            {
                DisplayIterationNum(i);
                for (int j = 0; j < amountOfVariables; j++)  
                {
                    //this can be shorter, will figure out how to make it so later on
                    double[] currentRowCoefficients = new double[coefficients.GetLength(0)];
                    for (int k = 0; k < currentRowCoefficients.Length; k++) currentRowCoefficients[k] = coefficients[j, k];
                    
                    Equation currentRowToEquation = new Equation(currentRowCoefficients, foundApproximations, constantTerms[j]);
                    double currentEquationResult = currentRowToEquation.Solve(j);
                    foundApproximations[j] = currentEquationResult;
                }

                if (CurrentSolutionIsAcceptable())
                {
                    Console.WriteLine("\nFound solution"); 
                    return foundApproximations;
                }
                if (countOfIncreasedAccDifference == 3 && !matrixIsNormalised)
                {
                    matrixNeedsToBeNormalised = true;
                    Console.WriteLine("\nMatrix needs to be normalised");
                    return new double[amountOfVariables];
                }
                //if (countOfIncreasedAccDifference == 3 && matrixIsNormalised)
                //{ "matrix is wrong blah-blah-blah" }
                
                //or rather make it two if-s:
                //if (countOfIncreasedAccDifference == 3)
                //{ if (matrixIsNormalised) {...} else {...} }
                
                foundApproximations.CopyTo(initialApproximation, 0);
                i++;
            }
        }

        private bool CurrentSolutionIsAcceptable()
        {   //I don't even wanna talk about everything that's going on here tbh
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
                //I will cut down on the amount of loops here lmao I promise
            }
            return isAcceptable;
        }

        private void CalculateDifferences()
        {
            Func<int, double> absoluteDifference = (idx) => 
                Math.Abs(foundApproximations[idx] - initialApproximation[idx]);
            
            for (int i = 0; i < currentAbsoluteDifferences.Length; i++)
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
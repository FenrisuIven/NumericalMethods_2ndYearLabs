using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Vector = MathNet.Numerics.LinearAlgebra.Single.Vector;

namespace _4MI_Laba2
{
    public class SimpleIterations
    {
        private SLE sle;
        
        private Vector<double> initialApproximation;
        private Vector<double> foundApproximations;
        
        private double accuracy;
        private int amountOfVariables;
        
        private Vector<double> prevAbsoluteDifferences;
        private Vector<double> currentAbsoluteDifferences;

        private int countOfIncreasedAccDifference = 0;
        private bool firstAccDiffComparison = true;
        
        private bool matrixIsNormalised = false;

        private bool enableOutput;
        private bool normaliseMatrix;
        public SimpleIterations(Vector<double> _approximations, double _accuracy, int _amountOfVariables, bool _enableOutput, bool _normaliseMatrix)
        {
            initialApproximation = Vector<double>.Build.Dense(_approximations.Count);
            _approximations.CopyTo(initialApproximation);
            
            amountOfVariables = _amountOfVariables;
            accuracy = _accuracy;

            foundApproximations = Vector<double>.Build.Dense(_approximations.Count);
            initialApproximation.CopyTo(foundApproximations);
            
            prevAbsoluteDifferences = Vector<double>.Build.Dense(amountOfVariables);
            currentAbsoluteDifferences = Vector<double>.Build.Dense(amountOfVariables);

            enableOutput = _enableOutput;
            normaliseMatrix = _normaliseMatrix;
        }
        
        public void Start(SLE sle)
        {
            int i = 0;
            while(true)  
            {
                Color.Write(0,$"\nІтерація №{i}. Початкове наближення: ");
                DisplayApproximations(i, false);
                for (int j = 0; j < amountOfVariables; j++)  
                {
                    Equation currentRowToEquation = new Equation(
                        sle.coefficients.Row(j), foundApproximations, sle.constantTerms[j]);
                    currentRowToEquation.varsAreNull = false;

                    double currentEquationResult = currentRowToEquation.Solve(j);
                    foundApproximations[j] = currentEquationResult;
                    
                    if (enableOutput) Color.WriteLine(2, currentRowToEquation.ToString() + $"\n\tx{j+1} = " + $"{currentEquationResult:0.0000}");
                }

                Color.Write(1, "Final approximations:\t\t   ");
                DisplayApproximations(i, true);
                
                if (CurrentSolutionIsAcceptable())
                {
                    Color.WriteLine(4,"\n- ! Знайдено розв'язок системи");
                    sle.variables = foundApproximations;
                    return;
                }
                
                if (countOfIncreasedAccDifference == 3)
                {
                    if (!matrixIsNormalised)
                    {
                        Color.WriteLine(3,"\n- ! Матрицю необхідно нормалізувати");
                        if (!normaliseMatrix) continue;
                        
                        Color.WriteLine(1,"\n- Нормалізуємо матрицю...");
                        sle.Normalise();
                        Vector<double> approximations = Vector<double>.Build.Dense(amountOfVariables, 1);
                        SimpleIterations si = new SimpleIterations(approximations, accuracy, amountOfVariables, enableOutput, normaliseMatrix);
                        si.matrixIsNormalised = true;
                        
                        if (enableOutput) Color.WriteLine(0,"- Виконано нормалізацію, розрахунок починається спочатку");
                        Console.WriteLine(sle);
                        si.Start(sle);
                        return;
                    }
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
                int idxColor = currentAbsoluteDifferences[i] < accuracy ? 4 : 
                    (currentAbsoluteDifferences[i] > prevAbsoluteDifferences[i] ? 3 : 5);
                Color.WriteLine(idxColor,$"| {foundApproximations[i]:0.00000} - {initialApproximation[i]:0.00000} | = " +
                                         $"{currentAbsoluteDifferences[i]:0.00000}{(currentAbsoluteDifferences[i] < accuracy ? " < ":" > ")}" +
                                         $"{accuracy:0.00###}");
                #endregion

                if (currentAbsoluteDifferences[i] > accuracy)
                {
                    if (isAcceptable) isAcceptable = false;
                    
                    if (firstAccDiffComparison) continue;
                    
                    if (Math.Abs(currentAbsoluteDifferences[i]) > Math.Abs(prevAbsoluteDifferences[i]))
                    {
                        Color.WriteLine(3, " - ! Різниця збільшилася");
                        countOfIncreasedAccDifference++;
                    }
                }
            }

            if (firstAccDiffComparison) firstAccDiffComparison = false;
            currentAbsoluteDifferences.CopyTo(prevAbsoluteDifferences);
            
            return isAcceptable;
        }

        private void CalculateDifferences()
        {
            Func<int, double> absoluteDifference = (idx) => 
                Math.Abs(foundApproximations[idx] - initialApproximation[idx]);
            
            for (int i = 0; i < currentAbsoluteDifferences.Count; i++)
                currentAbsoluteDifferences[i] = absoluteDifference(i);
        }
        
        private void DisplayApproximations(int idx, bool found)
        {
            Color.Write(1, "[ ");
            for (int i = 0; i < amountOfVariables; i++)
            {
                double approx = found ? foundApproximations[i] : initialApproximation[i];
                if (i != amountOfVariables - 1) Color.Write(1,$"{approx:0.0000} ; ");
                else Color.Write(1,$"{approx:0.0000} ");
            }
            Color.Write(0,"]\n");
        }
    }
}
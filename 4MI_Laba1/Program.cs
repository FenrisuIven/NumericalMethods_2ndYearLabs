using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

#region Інша функція
//static op function = (x) => (float)(Math.Cos(Math.Sin(Math.Pow(x, 3))) - .7f);
//static float[,] interval = { { (float)(-Math.PI / 2f), (float)(Math.PI / 2f) } };
#endregion

namespace _4MI_Laba1
{
    internal class Program
    {
        static Func<float, float> function = (x) => (float)(1.5f - Math.Pow(x, 1 - Math.Cos(x)));
        static float[,] interval = { { 0f, (float)(Math.PI * 5f) } };
        static float accuracy = .1f;
        static float[,] points;
        static int amountOfPoints = 10;

        static List<float> res = new List<float>();

        static bool enableOutput = true;

        static void Main(string[] args)
        {
            Output();
            Console.WriteLine("\nЕтап 1: Відокремлення коренів");

            float distance = Math.Abs(interval[0, 0] - interval[0, 1]);
            float step = distance / (float)amountOfPoints;
            points = new float[amountOfPoints + 1, 2];

            for (int i = 0; i <= amountOfPoints; i++)
            {
                points[i, 0] = interval[0, 0] + step * i;
                points[i, 1] = function(points[i, 0]);
                if (float.IsInfinity(points[i, 1])) points[i, 1] = float.PositiveInfinity;
                #region . "вивід Х та значення функції для нього" .
                Console.WriteLine($"x:{(points[i, 0] < 0 ? " " : "  ")}{points[i, 0]:0.0000}\t" +
                    $"y:{(points[i,1]<0 ? " " : "  ")}{points[i, 1]:0.0000}");
                #endregion
            }

            SecondStep_ChoosingInterval();

            Console.WriteLine("\nСписок коренів:");
            OutputRes();
            Console.ReadKey();
        }


        public static void SecondStep_ChoosingInterval()
        {
            Console.WriteLine("\nЕтап 2: Уточнення коренів на проміжку методом дотичних");

            float prev = points[0, 1];
            for (int i = 1; i <= amountOfPoints; i++)
            {
                

                if (prev * points[i, 1] < 0)
                { 
                    #region . "Відбулася зміна знаку" .
                    Console.WriteLine($"\nНа інтервалі " +
                        $"[{points[i - 1, 0]:0.00}, {points[i, 0]:0.00}] " +
                        $"відбулася зміна знаку з {(prev < 0 ? "мінуса" : "плюса")} на {(points[i, 1] > 0 ? "плюс" : "мінус")}");
                    #endregion

                    float foundRoot = SecondStep_ClarifyRoot(i, .1f, points[i - 1, 0]);
                    res.Add(foundRoot);
                    #region . "(збережено)" .
                    SetColor(Alerts[3]);
                    Console.WriteLine("(збережено)");
                    SetColor(Alerts[0]);
                    #endregion

                }
                prev = points[i, 1];
            }
        }

        public static float SecondStep_ClarifyRoot(int idx, float increase, float currentX)
        {
            while(true)
            {
                float nextX = XkPlusOne(currentX, increase);
                #region . "Значення функції для Хк та Хк+1" .
                if(enableOutput) {
                    SetColor(Alerts[3]);
                    Console.WriteLine($"Xk = {currentX:0.0#};\tXk+1 = {nextX:0.00};\t" +
                    $"F(Xk+1) = {(function(nextX) < 0 ? $"{function(nextX):0.0000}" : $" {function(nextX):0.0000}")}");
                    SetColor(Alerts[0]);
                }
                #endregion

                if (float.IsNaN(function(nextX)))
                {
                    #region . "Функція не визначена" .
                    SetColor(Alerts[1]);
                    Console.WriteLine($"- ! Функція не визначена. Пробуємо йти з іншого кінця інтервалу ");
                    SetColor(Alerts[0]);
                    #endregion
                    float temp = SecondStep_ClarifyRoot(idx, (-1f)*increase, points[idx, 0]);
                    return temp;
                }

                if (nextX > points[idx, 0]) 
                {
                    #region . "Перетин дотичної поза обраним інтервалом" .
                    SetColor(Alerts[1]);
                    Console.WriteLine(" - ! Точка перетину дотичної з ОХ більша за границю обраного інтервалу. Пробуємо йти з іншого його кінця");
                    SetColor(Alerts[0]);
                    #endregion
                    return SecondStep_ClarifyRoot(idx, (-1f) * increase, points[idx, 0]);
                }

                if (FuncValLessThanAcc(nextX)) 
                {
                    #region . "Знайдено корінь" .
                    SetColor(Alerts[2]);
                    Console.Write($" - Знайдено корінь: [{nextX:0.0000}, {function(nextX):0.0000}] ");
                    SetColor(Alerts[0]);
                    #endregion
                    return nextX; 
                }
                currentX = nextX;
            }
        }

        public static float XkPlusOne(float Xk, float increase) => 
            Xk - (function(Xk) / FDerivFromX(Xk, increase));
        public static float FDerivFromX(float X, float increase) => 
            (function(X + increase) - function(X)) / increase;


        #region Misc
        public static bool FuncValLessThanAcc(float point) =>
            Math.Abs(function(point)) < accuracy;

        static ConsoleColor[] Alerts = { ConsoleColor.Gray, ConsoleColor.Red, ConsoleColor.DarkGreen, ConsoleColor.DarkGray, ConsoleColor.White };
        public static ConsoleColor SetColor(ConsoleColor color) => Console.ForegroundColor = color;

        public static void Output()
        {
            Console.Write($"Задана функція:   1.5 - x^(1 - cos(x))\t");
            SetColor(Alerts[3]); Console.Write("1.5f - Math.Pow(x, 1 - Math.Cos(x))\n"); SetColor(Alerts[0]);
            Console.Write($"Заданий інтервал: [0, 5Pi]\t\t");
            SetColor(Alerts[3]); Console.Write($"[{ interval[0, 0]:0.0000}, { interval[0, 1]:0.0000}]\n"); SetColor(Alerts[0]);
        }

        public static void OutputRes()
        {
            foreach (float point in res)
            {
                bool anyNumBiggerThen10 = res.Any(elem => elem > 10) && point < 10;
                Console.WriteLine($"x: {point:0.0000};{(anyNumBiggerThen10 ? "  " : " ")}y: {function(point):0.0000}");
            }
        }
        #endregion
    }
}

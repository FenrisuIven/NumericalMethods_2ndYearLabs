using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4MI_Laba1
{
    internal class Program
    {
        public delegate float op(float x);
        
        static op function = (x) => (float)(1.5f - Math.Pow(x, 1 - Math.Cos(x)));
        static float[,] interval = { { 0f, (float)(Math.PI * 5f) } };
        #region Інша.функція
        //static op function = (x) => (float)(Math.Cos(Math.Sin(Math.Pow(x, 3)))-.7f);
        //static float[,] interval = { { (float)(- Math.PI / 2f), (float)(Math.PI / 2f) } };
        #endregion

        static float[,] points;

        static bool enableOutput = false;

        static void Main(string[] args)
        {
            Output();
            Console.WriteLine("\nЕтап 1: Відокремлення коренів");
            float distance = Math.Abs(interval[0, 0] - interval[0, 1]);
            float step = distance / 10.0f;
            points = new float[10, 2];

            for (int i = 0; i < 10; i++)
            {
                points[i, 0] = interval[0, 0] + step * i;
                points[i, 1] = function(points[i, 0]);
                Console.WriteLine($"x: {points[i, 0]:0.0000}\ty: {points[i, 1]:0.0000}");
            }


            List<float> listOfRoots = SecondStep_ChoosingInterval();


            Console.WriteLine("Список коренів:");
            foreach (float point in listOfRoots)
            {
                Console.WriteLine($"[{point:0.0000}; {function(point):0.0000}]");
            }
            Console.ReadKey();
        }

        public static void Output()
        {
            Console.WriteLine($"Задана функція: 1.5f - Math.Pow(x, 1 - Math.Cos(x), " + 
                $"Заданий інтервал: [{interval[0, 0]:0.0000}, {interval[0, 1]:0.0000}]");
        }

        public static List<float> SecondStep_ChoosingInterval()
        {
            Console.WriteLine("\nЕтап 2: Уточнення коренів на проміжку методом дотичних\n");

            float[] chosenInterval = new float[2];
            float prev = points[0, 1];
            List<float> res = new List<float>();

            for (int i = 1; i < 10; i++)
            {
                if ((prev < 0 && points[i, 1] > 0) || (prev > 0 && points[i, 1] < 0))
                { 
                    chosenInterval[0] = points[i - 1, 0];
                    chosenInterval[1] = points[i, 0];
                    #region Повідомлення
                    Console.WriteLine($"На інтервалі " +
                        $"[{chosenInterval[0]:0.00}, {chosenInterval[1]:0.00}] " +
                        $"відбулася зміна знаку з {(prev < 0 ? "мінуса" : "плюса")} на {(points[i, 1] > 0 ? "плюс" : "мінус")}");
                    #endregion

                    res.Add(SecondStep_ClarifyRoot(i - 1, .1f));

                }
                prev = points[i, 1];
            }
            return res;
        }

        public static float SecondStep_ClarifyRoot(int idx, float increase)
        {
            float currentX = points[idx, 0];
            while(currentX < points[idx + 1, 0])
            {
                float nextX = XkPlusOne(currentX, increase);
                if(enableOutput) Console.WriteLine($"X = {currentX:0.0#};\tXk+1 = {nextX:0.00};\tF(Xk+1) = {(nextX < 0 ? "не визначена" : $"{function(nextX):0.0000}")}");
                #region Для іншої функції
                //if(enableOutput) Console.WriteLine($"X = {currentX:0.0#};\tXk+1 = {nextX:0.00};\tF(Xk+1) = {function(nextX):0.0000}");
                #endregion
                if (Math.Abs(function(nextX)) < .1f) 
                {
                    Console.WriteLine($" - Знайдено корінь: [{nextX:0.0000}, {function(nextX):0.0000}]\n");
                    return nextX; 
                }
                currentX += increase;
            }
            return 0;
        }

        public static float XkPlusOne(float Xk, float increase) => Xk - (function(Xk) / FDerivFromX(Xk, increase));
        public static float FDerivFromX(float X, float increase) => (function(X + increase) - function(X)) / increase;
    }
}

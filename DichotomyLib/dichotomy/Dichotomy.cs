using DichotomyLib.exeption;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DichotomyLib
{
    /// <summary>
    /// Клас представляє метод дихотомії для знаходження найменшого значення функції
    /// </summary>
    /// <typeparam name="TFunction">Параметр типу функції, мінімум якої буде знаходитись</typeparam>
    public class Dichotomy<TFunction> where TFunction : AFunction, new()
    {
        private double minimum;
        private TFunction function;

        public Dichotomy()
        {
            function = new TFunction();
        }

        public Dichotomy(TFunction function)
        {
            this.function = function;
        }

        /// <summary>
        /// Отримання функції для якої здійснюється пошук мінімального значення
        /// </summary>
        public TFunction Function
        {
            get { return function; }
            set { function = value; }
        }

        /// <summary>
        /// Отримання мінімального заначення
        /// </summary>
        /// <param name="a">початок інтервалу</param>
        /// <param name="b">кінець інтервалу</param>
        /// <param name="accuracy">точність обчислень</param>
        /// <returns>дійсне мінімальне заначення</returns>
        public double GetMinimum(double a, double b, double accuracy = 0.01)
        {
            if(!IsOnlyOneMinimum(a, b, accuracy))
            {
                throw new IncorrectRangeException(a, b);
            }
            minimum = FindMinimum(a, b, accuracy);
            return minimum;
        }
       
        private bool IsOnlyOneMinimum(double a, double b, double accuracy)
        {
            IList<double> criticals = new List<double>();
            for(double i = a; i <= b; i++)
            {
                if(function.GetDerivative(i, accuracy) < accuracy)
                {
                    criticals.Add(i);
                }
            }

            Dictionary<double, int> valuesCount = new Dictionary<double, int>();
            foreach (double critical in criticals)
            {
                double key = function.GetValue(critical);
                int value = valuesCount.ContainsKey(key) ? valuesCount[key] : 0;
                valuesCount[key] = value + 1;
            }

            foreach(KeyValuePair<double, int> keyValue in valuesCount.Where(pair => pair.Value > 1))
            {
                if(keyValue.Key != valuesCount.Keys.Max())
                {
                    return false;
                }
            }
            return true;
        }

        private double FindMinimum(double a, double b, double accuracy)
        {
            double middle = (a + b) / 2;
            while (Math.Abs(b - a) > accuracy)
            {
                if (function.GetValue(middle - accuracy) < function.GetValue(middle + accuracy))
                {
                    b = middle;
                }
                else
                {
                    a = middle;
                }
                middle = (a + b) / 2;
            }
            return middle;
        }
    }
}

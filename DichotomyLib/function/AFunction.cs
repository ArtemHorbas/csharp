using System;

namespace DichotomyLib
{
    /// <summary>
    /// Абстрактний клас для представлення функції
    /// </summary>
    abstract public class AFunction
    {
        private double value;
        private double derivative;

        /// <summary>
        /// Отримання значення функції для переданого аргументу
        /// </summary>
        /// <param name="x">аргумент функції</param>
        /// <returns>дійсне значення функції</returns>
        public double GetValue(double x)
        {
            value = Calculate(x);
            return value;
        }

        abstract protected double Calculate(double x);

        public double GetDerivative(double x, double h)
        {
            derivative = CalculateNumericDerivative(x, h);
            return derivative;
        }

        private double CalculateNumericDerivative(double x, double h)
        {
            return (GetValue(x + h) - GetValue(x - h)) / (2 * h);
        }

        /// <summary>
        /// Виводить на консоль таблицю значень аргументу та функції
        /// </summary>
        /// <param name="name">ім'я функції</param>
        /// <param name="from">початок інтервалу</param>
        /// <param name="to">кінець інтервалу</param>
        /// <param name="step">крок</param>
        public void Test(string name, double from, double to, double step)
        {
            Console.WriteLine("*********** " + GetType() + " ***********");
            for (double x = from; x <= to; x += step)
                Console.WriteLine("x = {1}   \t {0}(x) = {2}", name, x, Calculate(x));
            Console.WriteLine();
        }
    }
}

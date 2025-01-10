using System.Collections.Generic;

namespace DichotomyLib
{
    /// <summary>
    /// Точка для задання функції g(x)
    /// </summary>
    public struct Point
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public  double X { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public  double Y { get; set; }

        public Point(double x, double y) 
        {
            X = x;
            Y = y;
        }
    }

    /// <summary>
    /// Клас для представлення функції з виконанняи інтерполяції методу поліномів Лагранжа
    /// </summary>
    public class GFunction : AFunction
    {
        private List<Point> points;
 
        public GFunction()
        {
            points = new List<Point>();
        }

        public GFunction(params Point[] points)
        {
            this.points = new List<Point>(points);
        }

        public List<Point> Points
        {
            get { return points; }
            set { points = value; }
        }

        /// <summary>
        /// Додає новий елемент до списку точок
        /// </summary>
        /// <param name="x">нове значення x</param>
        /// <param name="y">нове значення y</param>
        public void AddPoint(double x, double y)
        {
            points.Add(new Point(x, y));
        }

        override protected double Calculate(double x)
        {
            double result = 0;
            for (int i = 0; i < points.Count; i++)
            {
                double term = points[i].Y;
                for (int j = 0; j < points.Count; j++)
                {
                    if (i != j)
                    {
                        term *= (x - points[j].X) / (points[i].X - points[j].X);
                    }
                }
                result += term;
            }
            return result;
        }
    }
}

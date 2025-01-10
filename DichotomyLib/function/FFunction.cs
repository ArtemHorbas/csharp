using System;
using System.Collections.Generic;

namespace DichotomyLib
{
    /// <summary>
    /// Коефіцієнт функції f(x)
    /// </summary>
    public struct Coef
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public  int Index { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public  double Value { get; set; }

        public Coef(int index, double value) 
        {
            Index = index;
            Value = value;
        }
    }

    /// <summary>
    /// Клас для представлення функціі полінома
    /// </summary>
    public class FFunction : AFunction
    {
        private List<Coef> coefs;

        public FFunction()
        {
            coefs = new List<Coef>();
        }

        public FFunction(params Coef[] coefs)
        {
            this.coefs = new List<Coef>(coefs);
        }

        public List<Coef> Coefs
        {
            get { return coefs; }
            set { coefs = value; }
        }

        /// <summary>
        /// Додає новий коефіцієнт до функції
        /// </summary>
        /// <param name="index">індекс коефіцієнту в функції</param>
        /// <param name="value">значення коефіцієнту</param>
        public void AddCoef(int index, double value)
        {
            coefs.Add(new Coef(index, value));
        }

        override protected double Calculate(double x)
        {
            double sum = 0;
            for(int i = 0; i < coefs.Count; i++)
            {
                sum += coefs[i].Value * Math.Pow(x, coefs[i].Index);
            }
            return sum;
        }
    }
}

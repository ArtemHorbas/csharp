namespace DichotomyLib
{
    /// <summary>
    /// Клас для представлення функціі.
    /// Значення фукнції є різнецею між значенням функції полінома та функції з інтерполяцією
    /// </summary>
    public class MFunction : AFunction
    {
        private FFunction f;
        private GFunction g;

        public MFunction()
        {
            f = new FFunction();
            g = new GFunction();
        }

        public MFunction(FFunction f, GFunction g)
        {
            this.f = f;
            this.g = g;
        }

        public FFunction FFunction
        {
            get { return f; }
            set { f = value; }
        }

        public GFunction GFunction
        {
            get { return g; }
            set { g = value; }
        }

        override protected double Calculate(double x)
        {
            return f.GetValue(x) - g.GetValue(x);
        }
    }
}

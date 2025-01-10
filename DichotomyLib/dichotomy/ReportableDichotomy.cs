using System.IO;

namespace DichotomyLib
{
    /// <summary>
    /// Клас представляє метод дихотомії для знаходження найменшого значення функції з можливістю генерації звіту
    /// </summary>
    /// <typeparam name="TFunction">Параметр типу функції, мінімум якої буде знаходитись</typeparam>
    public class ReportableDichotomy<TFunction> : SerializableDichotomy<TFunction>, IReportable where TFunction : AFunction, new()
    {
        public ReportableDichotomy() : base() { }
        public ReportableDichotomy(TFunction f) : base(f) { }
        public ReportableDichotomy(string fileName) : base(fileName) { }

        /// <summary>
        /// Генерація звіту про пошук найменшого значення на основі вихідних даних
        /// </summary>
        /// <param name="fileName">назва файлу</param>
        /// <param name="a">початок інтервалу</param>
        /// <param name="b">кінець інтервалу</param>
        /// <param name="accuracy">точність обчислень</param>
        virtual public void GenerateReport(string fileName, double a, double b, double accuracy)
        {
            using(TextWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("<html>");
                writer.WriteLine("<head>");
                writer.WriteLine("<meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>");
                writer.WriteLine("<title>Report on the numerical search for a minimum using the dichotomy method</title>");
                writer.WriteLine("</head>");
                writer.WriteLine("</head>");
                writer.WriteLine("<body>");
                writer.WriteLine("<h1>Report</h1>");
                writer.WriteLine($"<h2>Numerical minimum :</h2>");
                writer.WriteLine($"<p>On interval [{a}, {b}] with accuracy {accuracy} equals: {GetMinimum(a, b, accuracy)}</p>");
                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
            }
        }
    }
}

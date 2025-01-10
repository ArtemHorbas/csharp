namespace DichotomyLib
{
    public interface IReportable
    {
        /// <summary>
        /// Генерує звіт про роботу програми в форматі HTML
        /// </summary>
        /// <param name="fileName">iм\'я файлу</param>
        /// <param name="a">початок інтервалу</param>
        /// <param name="b">кінець інтервалу</param>
        /// <param name="accuracy">точність обчислень</param>
        void GenerateReport(string fileName, double a, double b, double accuracy);
    }
}

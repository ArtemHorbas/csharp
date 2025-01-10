using System;
using DichotomyLib;
using DichotomyLib.exeption;

namespace DichotomyConsole
{
    class Program
    {
        static public void TestFunctions()
        {
            FFunction f = new FFunction(new Coef(0, 1), new Coef(1, -4), new Coef(2, 5));
            GFunction g = new GFunction(new Point(1, 1), new Point(2, 8), new Point(3, 6), new Point(4, 5));
            f.Test("F", 1, 5, 1);
            g.Test("G", 1, 5, 1);
        }

        static public void TestDichotomy()
        {
            Dichotomy<FFunction> fDichotomy = new SerializableDichotomy<FFunction>("FFunction.xml");
            Console.WriteLine(fDichotomy.GetMinimum(-1, 3)); // вийняток

            Dichotomy<MFunction> mDichotomy = new SerializableDichotomy<MFunction>("MFunction.xml");
            Console.WriteLine(mDichotomy.GetMinimum(0, 5, 0.0001));

            mDichotomy = new SerializableDichotomy<MFunction>("MFunction1.xml");
            Console.WriteLine(mDichotomy.GetMinimum(-1, 3)); // вийняток
        }

        static void Main()
        {
            try
            {
                TestFunctions();
                TestDichotomy();
            }
            catch (IncorrectRangeException ex)
            {
                Console.WriteLine("------------Виняток:------------");
                Console.WriteLine($"{ex.GetType().Name}: [{ex.Start}, {ex.End}]. Provide range with only one minimum value");
            }
            catch (Exception ex)
            {
                Console.WriteLine("------------Виняток:------------");
                Console.WriteLine(ex.GetType().Name);
                Console.WriteLine("-------------Змсiт:-------------");
                Console.WriteLine(ex.Message);
                Console.WriteLine("-------Трасування  стеку:-------");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}

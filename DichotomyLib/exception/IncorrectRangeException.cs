using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DichotomyLib.exeption
{
    public class IncorrectRangeException : Exception
    {
        public double Start { get; private set; }
        public double End { get; private set; }

        public IncorrectRangeException(double start, double end)
        {
            Start = start;
            End = end;
        }
    }
}

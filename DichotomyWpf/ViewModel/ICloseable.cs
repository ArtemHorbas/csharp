using System;

namespace DichotomyWpf.ViewModel
{
    public interface ICloseable
    {
        event Action ClosedRequest;
    }
}

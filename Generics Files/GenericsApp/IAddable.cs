using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsApp
{
    public interface IAddable<T>
    {
        T AddTo(T value);
    }
}

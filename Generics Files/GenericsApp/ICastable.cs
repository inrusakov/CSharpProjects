using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsApp
{
    public interface ICastable<T>
    {
        T Cast();
    }
}

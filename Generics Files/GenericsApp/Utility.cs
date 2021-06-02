using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsApp
{
    public static class Utility
    {
        public static AddableArray<T> GetAddableArray<T>(int size, Func<int, T> generator = null)
        {
            if (generator != null)
            {
                AddableArray<T> addableArray = new AddableArray<T>(size);
                T[] a = new T[size];
                for (int i = 0; i < size; i++)
                {
                    a = addableArray.AddTo(new T[1]{ generator(i)});
                    addableArray.Array[i] = a[0];
                }
                return addableArray;
            }
            else
            {
                AddableArray<T> addableArray = new AddableArray<T>(size);
                return addableArray;
            }
        }
    }
}

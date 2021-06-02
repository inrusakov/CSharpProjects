using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsApp
{
    public class AddableArray<T> : IAddable<T[]>
    {
        public T[] AddTo(T[] value)
        {
            if (value == null)
            {
                return array;
            }
            else
            {
                T[] newAr = new T[value.Length + array.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    newAr[i] = value[i];
                }
                for (int i = 0; i < array.Length; i++)
                {
                    newAr[i + value.Length] = array[i];
                }
                return newAr;
            }
        }

        private T[] array;
        
        public T[] Array
        {
            get { return array; }
            set { array = value; }
        }

        public AddableArray(T[] array)
        {
            this.array = array;
        }
        public AddableArray()
        {
            array = new T[0];
        }
        public AddableArray(int value)
        {
            array = new T[value];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsApp
{
    public class Number<T> : IAddable<int>, ICastable<int> where T : ICastable<int>
    {
        public int Cast()
        {
            int.TryParse(Value.Cast().ToString(), out int v);
            return v;
        }

        public int AddTo(int value)
        {
            try
            {
                return value += Value.Cast();
            }
            catch (ArgumentException)
            {
                return value;
            }
        }

        public T Value { get; set; }
        public Number(T value)
        {
            Value = value;
        }

    }
}

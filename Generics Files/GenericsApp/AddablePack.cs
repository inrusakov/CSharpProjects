using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsApp
{
    public class AddablePack<T> : Pack<T>, IAddable<Pack<T>>
    {
        public AddablePack(List<IAddable<T>> newOne) : base(newOne) { }

        public AddablePack() { }

        public Pack<T> AddTo(Pack<T> value)
        {
            if (value == null)
            {
                return null;
            }

            for (int i = 0; i < Values.Count; i++)
            {
                value.Values.Add(Values[i]);
            }
            return value;
        }
    }
}

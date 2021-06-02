using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GenericsApp
{
    public class Pack<T> : IEnumerable<IAddable<T>>
    {
        public T AddTo(T value)
        {
            return value;
        }
        public void Add(IAddable<T> element)
        {
            if (element != null)
            {
                Values.Add(element);
            }
        }
        public void RemoveLast()
        {
            if (Values.Count == 0)
            {
                throw new Exception();
            }
            if (Values.Count > 0)
            {
                Values.RemoveAt(Values.Count - 1);
            }
            sum = default(T);
        }
        public void Process(Action<IAddable<T>> action)
        {
            for (int i = 0; i < Values.Count; i++)
            {
                action(Values[i]);
            }
        }
        public T sum = default(T);
        public T Sum()
        {
            Process(x => sum = x.AddTo(sum));
            return sum;
        }
        private List<IAddable<T>> values;
        public List<IAddable<T>> Values
        {
            get { return values; }
            set { values = value; }
        }
        public Pack()
        {
            Values = new List<IAddable<T>>();
        }
        public Pack(List<IAddable<T>> newOne)
        {
            for (int i = 0; i < newOne.Count; i++)
            {
                Values.Add(newOne[i]);
            }
        }

        public IEnumerator<IAddable<T>> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }
    }
}

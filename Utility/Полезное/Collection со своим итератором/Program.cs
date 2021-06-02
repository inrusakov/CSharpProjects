using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Collection_со_своим_итератором
{
	public class Item
	{
		public int Weight { get; set; }
	}
	public class Collection<T> : IEnumerable<T> where T : Item
	{
		private List<T> items = new List<T>();
		public List<T> Items
		{
			get { return items; }
			set { items = value; }
		}

		public void Add(T item)
		{
			Items.Add(item);
		}
		public Collection(){}

		public IEnumerator<T> GetEnumerator() => new CollectionEnumerator<T>(items);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public class CollectionEnumerator<U> : IEnumerator<U> where U : T
		{
			List<U> collection;

			int pos = -1;
			public U Current => collection[pos];

			object IEnumerator.Current => Current;

			public void Dispose(){ }

			public bool MoveNext()
			{
				if (pos < collection.Count - 1)
				{
					++pos;
					return true;
				}
				return false;
			}

			public void Reset()
			{
				pos = -1;
			}

			public CollectionEnumerator(IEnumerable<U> items)
			{
				collection = items.ToList();
			}
		}
	}
}

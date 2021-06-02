using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    [Serializable]
	public class RusakovWardrobe
    {
		public RusakovWardrobe(long value)
		{
			Volume = value;
		}

		public RusakovWardrobe()
		{

		}

		private long volume;

		public long Volume
		{
			get { return volume; }
			set { volume = value; }
		}

		private List<item> items;

		public List<item> Items
		{
			get { return items; }
			set { items = value; }
		}


	}
}

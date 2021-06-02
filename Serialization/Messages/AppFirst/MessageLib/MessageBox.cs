using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MessageLib
{
    public class MessageBox: IEnumerable<Message>
	{
		private List<Message> messages = new List<Message>();
		public List<Message> Messages
		{
			get { return messages; }
			set { messages = value; }
		}

		public void ReceiveMail(Message msg)
		{
			Messages.Add(msg);
		}

		public IEnumerator<Message> GetEnumerator()
		{
			return ((IEnumerable<Message>)Messages).OrderBy(x => x.ReceiveDate).Reverse().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<Message>)Messages).OrderBy(x => x.ReceiveDate).Reverse().GetEnumerator();
		}
	}
}

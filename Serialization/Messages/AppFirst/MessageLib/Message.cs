using System;

namespace MessageLib
{
    public class Message
    {
		public Message(string content, DateTime send)
		{
			Content = content;
			SendDate = send;
			receiveDate = SendDate.AddSeconds(1);
		}

		private string content;
		public string Content
		{
			get { return content; }
			set 
			{
				if (value.Length <=80)
				{
					content = value;
				}
				else
				{
					throw new ArgumentException("Wrong content Length");
				}
			}
		}

		private DateTime sendDate;
		public DateTime SendDate
		{
			get { return sendDate; }
			set { sendDate = value; }
		}

		private DateTime receiveDate;
		public DateTime ReceiveDate
		{
			get { return receiveDate; }
			set { receiveDate = value; }
		}
		public Message()
		{

		}

		public override string ToString()
		{
			return $"Mail: Content = {Content}, SendDate = {SendDate}, ReceiveDate = {ReceiveDate}";
		}

	}
}

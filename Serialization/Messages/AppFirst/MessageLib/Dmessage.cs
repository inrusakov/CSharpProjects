using System;

namespace MessageLib
{
    public class Dmessage : Message
    {
        public Dmessage()
        {

        }

        public Dmessage(string content, DateTime sendTime, int hours) : base(content, sendTime)
        {
            Hours = hours;
            ReceiveDate = sendTime.AddHours(-hours);
        }

        public int Hours { get; set; }

        public new DateTime ReceiveDate { get; set; }

        public override string ToString()
        {
            return $"D-Message: Content = {Content}, SendDate = {SendDate}, Hours = {Hours}, ReceiveDate = {ReceiveDate}";
        }
    }
}
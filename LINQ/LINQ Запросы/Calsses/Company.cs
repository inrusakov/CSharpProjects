using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    class Company : IComparable<Company>, IComparable<Person>
    {
        public int EstablishedYear { get; }
        public string Name { get; }
        public decimal Capitalization { get; }
        public Person Head { get; }
        public static Company operator +(Company c1, Company c2)
        {
            string name = c1.Name;
            decimal cap = c1.Capitalization + c2.Capitalization;
            Person head = c1.Head.NetWorth >= c2.Head.NetWorth ? c1.Head : c2.Head;
            int estYear = c1.EstablishedYear;
            return new Company(name, estYear, cap, head);
        }
        public static bool operator >(Company c1, Company c2)
        {
            if (c1.Capitalization > c2.Capitalization)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Company c1, Company c2)
        {
            if (c1.Capitalization < c2.Capitalization)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Company c1, Company c2)
        {
            if (c1.Capitalization <= c2.Capitalization)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Company c1, Company c2)
        {
            if (c1.Capitalization >= c2.Capitalization)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Company c1, Company c2)
        {
            if (c1.Capitalization == c2.Capitalization)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Company c1, Company c2)
        {
            if (c1.Capitalization != c2.Capitalization)
            {
                return true;
            }
            return false;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public Company(string name, int estYear, decimal cap, Person head)
        {
            Name = name;
            EstablishedYear = estYear;
            Capitalization = cap;
            Head = head;
        }

        public override string ToString() => $"{Name}({EstablishedYear}), Head - {Head}, Cap - ${Capitalization}";

        public int CompareTo(Company other) => this.Capitalization >= other.Capitalization ? 1 : -1;
        public int CompareTo(Person other) => this.Capitalization >= other.NetWorth ? 1 : -1;
    }
}

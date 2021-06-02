using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    class Person: IComparable<Person>, IComparable<Company>
    {
		public string Name { get; }
        public decimal NetWorth { get; }

        public Person(string name, decimal worth)
        {
            Name = name;
            NetWorth = worth;
        }
        public override string ToString() => $"{Name}: ${NetWorth}";

        //public int CompareTo(object obj)
        //{
        //    Person p = obj as Person;
        //    return this.NetWorth >= p.NetWorth ? 1 : -1;
        //}

        public int CompareTo(Person other) => this.NetWorth >= other.NetWorth ? 1 : -1;

        public int CompareTo(Company other) => this.NetWorth >= other.Capitalization ? 1 : -1;
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseTask2
{
    class Good : IEntity
    {
        public long Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string Category { get; }

        public Good(long id, string name, string description, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
        }

        public override string ToString()
        {
            return $"id: {Id}," +
                $" Name: {Name}," +
                $" Description: {Description}," +
                $" Category: {Category},";
        }
    }
}

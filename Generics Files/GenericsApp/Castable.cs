using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsApp
{
    class Castable :ICastable<int>
    {
        public int Value { get; set; }

        public int Cast()
        {
            return Value;
        }
    }
}

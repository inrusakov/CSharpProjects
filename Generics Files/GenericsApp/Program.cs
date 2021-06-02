using System;
using Xunit;

namespace GenericsApp
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("GenericsApp");
            AddablePackTest(10);
            AddablePackTest(-8);
            PackOfArrayTest();
            var pack = new AddablePack<int>();
        }
        public static void AddablePackTest(int value)
        {
            var pack = new AddablePack<int>();
            Assert.True(pack is Pack<int>);
            Assert.True(pack is IAddable<Pack<int>>);
            pack.Add(new AddableInteger(-2));
            pack.Add(new AddableInteger(5));
            pack.Add(new AddableInteger(value));

            Assert.Null(pack.AddTo(null));
            var another = new AddablePack<int>();
            pack.AddTo(another);
            Pack<int> added = pack.AddTo(another);
            pack.AddTo(new Pack<int>());

            Assert.Equal(added, another);
            Assert.Equal(added.Sum(), (-2 + 5 + value) * 2);
        }

        [Fact]
        public static void PackOfArrayTest()
        {
            var pack = new Pack<int[]>();
            pack.Add(new AddableArray<int>(new[] { 1, 2, 3 }));
            pack.Add(new AddableArray<int>(new[] { 4, 5, 6, 7 }));
            var array = pack.Sum();
            Assert.Equal(7, array.Length);
            for (int i = 0; i < array.Length; i++)
                Assert.Equal(array[i], i + 1);
        }
        class AddableNullableInteger : IAddable<int?>
        {
            public int? Value { get; set; }

            public AddableNullableInteger(int? value)
            {
                Value = value;
            }

            public int? AddTo(int? value)
            {
                return Value + (value == null ? 0 : value.Value);
            }
        }

        class AddableInteger : IAddable<int>
        {
            public int Value { get; set; }

            public AddableInteger(int value)
            {
                Value = value;
            }

            public int AddTo(int value)
            {
                return Value + value;
            }
        }
    }
}

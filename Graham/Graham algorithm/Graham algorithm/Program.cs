using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class Program
{
    public class MyStack<T>
    {
        private T[] items; // элементы стека
        private int count;  // количество элементов
        public MyStack()
        {
            items = new T[maxCapacity];
        }
        public MyStack(int length)
        {
            items = new T[length - 1];
        }

        public const int maxCapacity = 1000;

        public bool IsEmpty
        {
            get { return count == 0; }
        }

        public int size()
        {
            return count;
        }

        public T First()
        {
            return items[0];
        }

        public void Push(T item)
        {
            if (count == items.Length)
                throw new InvalidOperationException("Stack Overflow");
            items[count++] = item;
        }

        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Stack is empty");
            T item = items[--count];
            items[count] = default(T);
            return item;
        }

        public T Top()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Stack is empty");
            return items[count - 1];
        }

        public T NextToTop()
        {
            if (IsEmpty && count > 1)
                throw new InvalidOperationException("There is less than 2 elements");
            return items[count - 2];
        }
    }
    public class Point
    {
        private int x, y;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Point(int a, int b)
        {
            X = a;
            Y = b;
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }

        public int Orientation(Point q, Point r, bool clock)
        {
            int val = (q.Y - Y) * (r.X - q.X) - (q.X - X) * (r.Y - q.Y);
            if (val == 0) return 0;
            if (clock == true)
            {
                return (val > 0) ? 2 : 1;
            }
            else
            {
                return (val > 0) ? 1 : 2;
            }
        }
        public int Distance(Point target)
        {
            return (X - target.X) * (X - target.X) + (Y - target.Y) * (Y - target.Y);
        }
    }
    public class PointsComparer : Comparer<Point>
    {
        private readonly Point Root;
        private readonly bool clock;
        public PointsComparer(Point root, bool clock)
        {
            Root = root;
            this.clock = clock;
        }

        public override int Compare(Point x, Point y)
        {
            if (x == y)
            {
                return 0;
            }

            int comp = Root.Orientation(x, y, clock);
            if (comp == 0)
                return (Root.Distance(y) >= Root.Distance(x)) ? -1 : 1;

            return (comp == 2) ? -1 : 1;
        }
    }
    static void Main(string[] args)
    {
        List<Point> givenPoints = new List<Point>();
        string path1 = args[2];
        string path2 = args[3];
        using (StreamReader sr = new StreamReader(path1))
        {
            int counter = 0;
            int.TryParse(sr.ReadLine(), out counter);
            for (int i = 0; i < counter; i++)
            {
                string[] parts = sr.ReadLine().Split(' ');
                givenPoints.Add(new Point(int.Parse(parts[0]), int.Parse(parts[1])));
            }
        }
        MyStack<Point> points = new MyStack<Point>();
        try
        {
            if (args[0] == "cw")
            {
                points = ConvexHull(givenPoints, true);
            }
            else if (args[0] == "cc")
            {
                points = ConvexHull(givenPoints, false);
            }
            else
            {
                throw new ArgumentException("Wrong clock direction param!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        if (args[1] == "plain")
        {
            int size = points.size();
            Console.WriteLine(points.size());
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(points.Pop());
            }
        }
        if (args[1] == "wkt")
        {
            int size = givenPoints.Count;
            Console.Write($"MULTIPOINT (({givenPoints.First()})");
            for (int i = 1; i < size; i++)
            {
                Console.Write($", ({givenPoints[i]})");
            }
            Console.WriteLine(")");

            int sizeOfStack = points.size();
            Console.Write($"POLYGON (({points.Pop()})");
            for (int i = 0; i < sizeOfStack-1; i++)
            {
                Console.Write($", ({points.Pop()})");
            }
            Console.Write(")");
        }
    }

    public static MyStack<Point> ConvexHull(List<Point> points, bool clock)
    {
        if (points.Count < 3)
        {
            throw new Exception("Not enought points for convex hull");
        }

        List<Point> sortedPoints = points.OrderBy(p => p.Y).ThenBy(p => p.X).ToList();
        List<Point> mostLeftPoint = new List<Point>();
        mostLeftPoint.Add(sortedPoints.First());
        sortedPoints = sortedPoints.Skip(1).OrderBy(p => p, new PointsComparer(mostLeftPoint.First(), clock)).ToList();
        points = mostLeftPoint.Concat(sortedPoints).ToList();

        MyStack<Point> stack = new MyStack<Point>();

        stack.Push(points[1]);
        stack.Push(points[2]);

        for (int i = 3; i < points.Count; i++)
        {
            while (stack.size() > 1 && stack.NextToTop().Orientation(stack.Top(), points[i], clock) != 2)
            {
                stack.Pop();
            }
            stack.Push(points[i]);
        }
        stack.Push(points[0]);
        return stack;
    }
}
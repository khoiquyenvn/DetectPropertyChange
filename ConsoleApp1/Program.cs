using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            SomeCustomClass a = new SomeCustomClass()
            {
                AnotherChildClasses = new List<AnotherChildClass> {
                     new AnotherChildClass { n = 1, m = 2 },
                     new AnotherChildClass { n = 11, m = 22 }
                 }
            };
            SomeCustomClass b = new SomeCustomClass()
            {
                AnotherChildClasses = new List<AnotherChildClass> {
                    new AnotherChildClass { n = 21, m = 22 },
                    new AnotherChildClass { n = 1, m = 2 }
                }
            };
            a.x = 100;
            b.Child.u = 51;

            List<Variance> compareResult = Extentions.DetailedCompare(a,b);


            Console.ReadLine();
        }

        private static void Test_Compare2List()
        {
            var lst1 = new List<AnotherChildClass> { new AnotherChildClass { n = 1, m = 2 }, new AnotherChildClass { n = 11, m = 22 } };
            var lst2 = new List<AnotherChildClass> { new AnotherChildClass { n = 21, m = 22 }, new AnotherChildClass { n = 1, m = 2 } };

            var diff1 = lst1.Except(lst2).ToList();
            var diff2 = lst2.Except(lst1).ToList();
        }
    }

    class SomeCustomClass
    {
        public int x = 12;
        public int y = 13;

        public ChildClass Child { get; set; } = new ChildClass();

        public List<AnotherChildClass> AnotherChildClasses { get; set; } = new List<AnotherChildClass>();
    }

    partial class ChildClass
    {
        public int u = 22;
        public int v = 23;
    }

    // try to implement IEquatable in another place (partial)
    partial class ChildClass : IEquatable<ChildClass>
    {
        public bool Equals(ChildClass other)
        {
            return this.u == other.u && this.v == other.v;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ChildClass);
        }

        public override int GetHashCode()
        {
            return u.GetHashCode() ^ v.GetHashCode();
        }
    }

    partial class AnotherChildClass : IEquatable<AnotherChildClass>
    {
        public int n = 32;
        public int m = 33;
        public bool Equals(AnotherChildClass other)
        {
            return this.n == other.n && this.m == other.m;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as AnotherChildClass);
        }

        public override int GetHashCode()
        {
            return n.GetHashCode() ^ m.GetHashCode();
        }
    }
}

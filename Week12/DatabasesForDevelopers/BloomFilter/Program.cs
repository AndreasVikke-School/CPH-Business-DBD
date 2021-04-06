using System;

namespace BloomFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            BFilter bFilter = new BFilter();

            bFilter.AddItem("test");
            bFilter.AddItem("test2");
            bFilter.AddItem("test3");
            bFilter.AddItem("test4");
            bFilter.AddItem("test5");

            Console.WriteLine($"Is 'whatever' in filter: {bFilter.PossiblyExists("whatever")}");
            Console.WriteLine($"Is 'test' in filter: {bFilter.PossiblyExists("test")}");
            Console.WriteLine($"Is 'test2' in filter: {bFilter.PossiblyExists("test2")}");
            Console.WriteLine($"Is 'test3' in filter: {bFilter.PossiblyExists("test3")}");
            Console.WriteLine($"Is 'test4' in filter: {bFilter.PossiblyExists("test4")}");
            Console.WriteLine($"Is 'test5' in filter: {bFilter.PossiblyExists("test5")}");
            Console.WriteLine($"Is 'test6' in filter: {bFilter.PossiblyExists("test6")}");
        }
    }
}

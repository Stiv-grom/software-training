using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Linq;

namespace HW2_ContainerImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HW2: Implement Javascript-like associated container");

            ContainerRunner.StringObjectContainer();
            ContainerRunner.StringObjectContainerWithoutGaps();
            ContainerRunner.UseFirstHashAlgorithm();
            ContainerRunner.UseSecondHashAlgorithm();
            ContainerRunner.UseThirdHashAlgorithm();
            ContainerRunner.UseBadHash();
            Console.ReadLine();

            /*uncomment for benchmark run
            var summary = BenchmarkRunner.Run<ContainerRunner>();
            
            Benchmark results
            |              Method |     Mean |     Error |   StdDev |   Median |
            |-------------------- |---------:|----------:|---------:|---------:|
            |  BenchmarkFirstHash | 359.1 ms | 17.649 ms | 51.20 ms | 339.1 ms |
            | BenchmarkSecondHash | 350.7 ms |  6.992 ms | 18.90 ms | 343.8 ms |
            |  BenchmarkThirdHash | 363.2 ms |  8.219 ms | 22.36 ms | 359.3 ms |
            |    BenchmarkBadHash | 290.8 ms |  5.777 ms | 11.27 ms | 286.5 ms |
 
             ((key >> 16) ^ key) * 0x45d9f3b - is the most efficient hash-calculation from the proposed options
             BadHash is fast enough, but results are unevenly distributed and a lot of collisions are created, 
             so Find algorithm will be unefficient

            Using of Array without gaps is unefficient and takes too long time
             */
        }
    }

    public class ContainerRunner {
        private static Random random = new Random();
        private const int containerSize = 100000000; // 1e8

        public static void StringObjectContainer()
        {
            Console.WriteLine("Container with generic hash implementation is in progress");
            Container<string, object> container = new Container<string, object>(containerSize);

            for (int i = 0; i < containerSize; i++)
            {
                container.Add(RandomString(5), RandomString());
            }
            Console.WriteLine("Check was completed");
        }

        public static void StringObjectContainerWithoutGaps()
        {
            Console.WriteLine("Array without gaps is in progress");
            int million = 1000000; // 1e6
            int arrayContainerSize = containerSize < million ? containerSize : million;

            if (containerSize > million) //1e6
            {
                Console.WriteLine("Array without gaps usage is limited to 1e6");
            }

            ContainerWithoutGaps<string, object> container = new ContainerWithoutGaps<string, object>(arrayContainerSize);

            for (int i = 0; i < arrayContainerSize; i++)
            {
                container.AddLast(RandomString(5), RandomString());
            }
            Console.WriteLine("Check was completed");
        }

        public static void UseFirstHashAlgorithm()
        {
            Console.WriteLine("Using first algorithm: 101 * ((key >> 24) + 101 * ((key >> 16) + 101 * (key >> 8))) + key;");
            Container<int, object> container = new Container<int, object>(containerSize);

            for (int i = 0; i < containerSize; i++)
            {
                container.Add(i, RandomString(), HashMethodImplementation.GenerateHashCodeFirstCase);
            }
            Console.WriteLine("First algorithm usage was completed");
        }

        public static void UseSecondHashAlgorithm()
        {
            Console.WriteLine("Using second algorithm: ((key >> 16) ^ key) * 0x45d9f3b");
            Container<int, object> container = new Container<int, object>(containerSize);

            for (int i = 0; i < containerSize; i++)
            {
                container.Add(i, RandomString(), HashMethodImplementation.GenerateHashCodeSecondCase);
            }
            Console.WriteLine("Second algorithm usage was completed");
        }

        public static void UseThirdHashAlgorithm()
        {
            Console.WriteLine("Using third algorithm: hash = key");
            Container<int, object> container = new Container<int, object>(containerSize);

            for (int i = 0; i < containerSize; i++)
            {
                container.Add(i, RandomString(), HashMethodImplementation.GenerateHashCodeSecondCase);
            }
            Console.WriteLine("Third algorithm usage was completed");
        }

        public static void UseBadHash()
        {
            Console.WriteLine("Using bad hash algorithm: hash = key.ToString().Length * 2");
            Container<int, object> container = new Container<int, object>(containerSize);

            for (int i = 0; i < containerSize; i++)
            {
                container.Add(i, RandomString(), HashMethodImplementation.BadHashExample);
            }
            Console.WriteLine("Bad hash usage was completed");
        }

        public static string RandomString(int length = 50)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #region Benchmarks

        [Benchmark]
        public void BenchmarkFirstHash()
        {
            UseFirstHashAlgorithm();
        }

        [Benchmark]
        public void BenchmarkSecondHash()
        {
            UseSecondHashAlgorithm();
        }

        [Benchmark]
        public void BenchmarkThirdHash()
        {
            UseThirdHashAlgorithm();
        }


        [Benchmark]
        public void BenchmarkBadHash()
        {
            UseBadHash();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_ContainerImplementation
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("HW2: Implement Javascript-like associated container");

            int containerSize = 1000;

            StringObjectContainer(containerSize);
            UseFirstHashAlgorithm(containerSize);
            UseFSecondHashAlgorithm(containerSize);
            UseThirdtHashAlgorithm(containerSize);

            Console.ReadLine();
        }

        static void StringObjectContainer(int containerSize)
        {
            Console.WriteLine("Using first algorithm: 101 * ((key >> 24) + 101 * ((key >> 16) + 101 * (key >> 8))) + key;");
            Container<string, object> container = new Container<string, object>(containerSize);

            for (int i = 0; i < containerSize; i++)
            {
                container.Add(RandomString(5), RandomString(), HashMethodImplementation.GenerateHashCodeFirstCase);
            }
        }

        static void UseFirstHashAlgorithm(int containerSize)
        {
            Console.WriteLine("Using first algorithm: 101 * ((key >> 24) + 101 * ((key >> 16) + 101 * (key >> 8))) + key;");
            Container<int, object> container = new Container<int, object>(containerSize);

            for (int i = 0; i < containerSize; i++)
            {
                container.Add(i, RandomString(), HashMethodImplementation.GenerateHashCodeFirstCase);
            }
        }

        static void UseFSecondHashAlgorithm(int containerSize)
        {
            Console.WriteLine("Using second algorithm: ((key >> 16) ^ key) * 0x45d9f3b");
            Container<int, object> container = new Container<int, object>(containerSize);

            for (int i = 0; i < containerSize; i++)
            {
                container.Add(i, RandomString(), HashMethodImplementation.GenerateHashCodeSecondCase);
            }
        }

        static void UseThirdtHashAlgorithm(int containerSize)
        {
            Console.WriteLine("Using third algorithm: hash = key");
            Container<int, object> container = new Container<int, object>(containerSize);

            for (int i = 0; i < containerSize; i++)
            {
                container.Add(i, RandomString(), HashMethodImplementation.GenerateHashCodeSecondCase);
            }
        }

        public static string RandomString(int length = 50)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

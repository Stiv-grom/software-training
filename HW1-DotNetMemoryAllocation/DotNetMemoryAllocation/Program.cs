using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetMemoryAllocation
{
    class Program
    {
        const long BILLION = 1000000000;

        static void Main(string[] args)
        {
            Console.WriteLine(@"Starting heap size: {0} b", GC.GetTotalMemory(false));
            long maxHeapSize = GC.GetTotalMemory(false);
            List<UserInfo> list = new List<UserInfo>();

            bool isExceptionThrown = false;
            while (!isExceptionThrown)
            {
                try
                {
                    for (long i = 0; i < long.MaxValue; i++)
                    {
                        UserInfo user = new UserInfo("Dm", i);
                        list.Add(user);
                        if (i % BILLION == 0)
                        {
                            if (maxHeapSize < GC.GetTotalMemory(false))
                            {
                                maxHeapSize = GC.GetTotalMemory(false);
                            }
                            Console.WriteLine(@"Now heap size: {0} b for {1} billion created objects", GC.GetTotalMemory(false), i / BILLION);
                        }
                    }

                }
                catch (OutOfMemoryException)
                {
                    isExceptionThrown = true;
                    maxHeapSize = GC.GetTotalMemory(false);
                    Console.WriteLine("OutOfMemoryException was thrown");
                    Console.WriteLine(@"Max heap size: {0} b", maxHeapSize);
                    break;
                }
            }

            Console.ReadLine();
        }
    }

    class UserInfo
    {
        public string Name { set; get; }
        public long Age { set; get; }

        public UserInfo(string Name, long Age)
        {
            this.Name = Name;
            this.Age = Age;
        }
    }
}

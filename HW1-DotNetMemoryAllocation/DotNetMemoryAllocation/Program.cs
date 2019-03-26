using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Devices;

namespace DotNetMemoryAllocation
{
    class Program
    {
        const long BILLION = 1000000000;

        static void Main(string[] args)
        {
            ComputerInfo ci = new ComputerInfo();
            Console.WriteLine(@"Total RAM memory: {0} b", ci.TotalPhysicalMemory);

            // Task #1
            MaxMemory();

            // Task #2
            SingleObject();

            Console.ReadLine();
        }

        public static void MaxMemory()
        {
            Console.WriteLine(@"Task #1: Amount of available memory to allocate");
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

            // collect all the garbage
            GC.Collect(0, GCCollectionMode.Forced);
            GC.Collect(1, GCCollectionMode.Forced);
            GC.Collect(2, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();

            Console.WriteLine(@"Cleaned heap size: {0} b", GC.GetTotalMemory(false));
        }

        public static void SingleObject()
        {
            Console.WriteLine(@"Task #2: Max size of single block available in heap to allocate");
            Console.WriteLine(@"Starting heap size: {0} b", GC.GetTotalMemory(false));
            long startHeapSize = GC.GetTotalMemory(false);
            long maxHeapSize = GC.GetTotalMemory(false);
            bool isExceptionThrown = false;
            while (!isExceptionThrown)
            {
                StringBuilder myStringBuilder = new StringBuilder();
                try
                {
                    for (long i = 0; i < long.MaxValue; i++)
                    {
                        myStringBuilder.Insert(myStringBuilder.Length, i.ToString()); 
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
                    Console.WriteLine(@"Object max size: {0} b", maxHeapSize - startHeapSize);
                    break;
                }
            }

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

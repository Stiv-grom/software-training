using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW5_LockFreeLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"HW5 - Create linked-list container that supports lock-free adding to list head");

            CustomLinkedList customList = new CustomLinkedList();
            customList.AddFirst(new { property = "test" });
            customList.AddFirst(new { property = "test2" });
            customList.AddFirst(new { property = "test3" });

            CustomLinkedList customListLockFree = new CustomLinkedList();
            customListLockFree.AddFirstLockFree(new { property = "test" });
            customListLockFree.AddFirstLockFree(new { property = "test2" });
            customListLockFree.AddFirstLockFree(new { property = "test3" });

            bool comparingResult = customList.Equals(customListLockFree);

            Console.WriteLine(@"Lock-free and general method results of adding are {0} equal", comparingResult ? "" : "not");

            Console.ReadLine();
        }
    }
}

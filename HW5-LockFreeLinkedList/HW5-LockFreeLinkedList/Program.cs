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
            customList.InsertFront(new { property = "test" });
            customList.InsertFront(new { property = "test2" });
            customList.InsertFront(new { property = "test3" });

            Console.ReadLine();
        }
    }
}

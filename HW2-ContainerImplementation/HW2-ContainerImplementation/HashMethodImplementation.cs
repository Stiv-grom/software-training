using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_ContainerImplementation
{
    public static class HashMethodImplementation
    {
        public static int GenerateHashCodeFirstCase(int key)
        {
            var x = 101 * ((key >> 24) + 101 * ((key >> 16) + 101 * (key >> 8))) + key;
            return x;
        }

        public static int GenerateHashCodeSecondCase(int key)
        {
            var x = ((key >> 16) ^ key) * 0x45d9f3b;
            return x;
        }

        public static int GenerateHashCodeThirdCase(int key)
        {
            return key;
        }

        public static int BadHashExample(int key)
        {
            return key % 25;
        }
    }
}

using System;

namespace HW2_ContainerImplementation
{
    public struct Entry<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
    }
}

using System;

namespace HW2_ContainerImplementation
{
    public struct Entry<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public int hashCode;

        public static int GenerateClassicHashCode(K key, int size)
        {
            int position = key.GetHashCode() % size;
            return Math.Abs(position);
        }
    }
}

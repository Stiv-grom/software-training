using System;
using System.Collections.Generic;

namespace HW2_ContainerImplementation
{
    public class Container<K, V>
    {
        private readonly int size;
        private readonly LinkedList<Entry<K, V>>[] items;

        public Container(int size)
        {
            this.size = size;

            items = new LinkedList<Entry<K, V>>[size];

        }

        public void Add(K key, V value, Func<int, int> hashGenerationFunction = null)
        {
            int position = GetArrayPosition(key, hashGenerationFunction);

            LinkedList<Entry<K, V>> linkedList = GetLinkedList(position);
            Entry<K, V> item = new Entry<K, V>()
            {
                Key = key,
                Value = value
            };

            linkedList.AddLast(item);
        }

        protected int GetArrayPosition(K key, Func<int, int> hashGenerationFunction)
        {
            int position = 0;
            // I didn't override object.GetHashCode for checking different HashCode approaches simultaneously
            if (hashGenerationFunction != null && typeof(K) == typeof(Int32))
            {
                position = hashGenerationFunction(Convert.ToInt32(key)) % size;
            }
            else
            {
                position = key.GetHashCode() % size;
            }
            return Math.Abs(position);
        }

        protected LinkedList<Entry<K, V>> GetLinkedList(int position)
        {
            LinkedList<Entry<K, V>> linkedList = items[position];
            if (linkedList == null)
            {
                linkedList = new LinkedList<Entry<K, V>>();
                items[position] = linkedList;
            }

            return linkedList;
        }
    }
}

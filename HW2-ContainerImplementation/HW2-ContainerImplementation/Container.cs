using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_ContainerImplementation
{
    public class Container<K, V>
    {
        private readonly int size;
        private readonly LinkedList<Entry<K, V>>[] items;
        //private readonly IList<Entry<K, V>>[] items;
        //private readonly IList<Entry<K, V>> items2;
        //private readonly ICollection<Entry<K, V>> items;
        //private readonly LinkedList<KeyValue<K, V>>[] items;

        private const int MAX_SIZE_FOR_ARRAY = 1; // 1e6

        public Container(int size)
        {
            this.size = size;

            // use LinkedList for size > 1e6
            if (size > MAX_SIZE_FOR_ARRAY)
            {
                items = new LinkedList<Entry<K, V>>[size];
                //items = new List<Entry<K, V>>[size];
            }
            else
            {
                //items = new Entry<K, V>[size][];
                //items2 = new Entry<K, V>[size];
            }
        }

        public void Add(K key, V value, Func<int, int> HashGenerationFunction)
        {
            int position = GetArrayPosition(key);
            LinkedList<Entry<K, V>> linkedList = GetLinkedList(position);
            Entry<K, V> item = new Entry<K, V>() { 
                Key = key, 
                Value = value
            };

            if (typeof(K) == typeof(int))
            {
                item.hashCode = HashGenerationFunction(Convert.ToInt32(key));
            }

            linkedList.AddLast(item);

            //IList<Entry<K, V>> linkedList = GetLinkedList(position);
            //linkedList.Add(item);

        }

        protected int GetArrayPosition(K key)
        {
            int position = key.GetHashCode() % size;
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

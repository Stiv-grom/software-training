using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_ContainerImplementation
{
    public class ContainerWithoutGaps<K, V>
    {
        private readonly int size;
        private readonly Entry<K, V>[] items;

        public ContainerWithoutGaps(int size)
        {
            this.size = size;
            items = new Entry<K, V>[size];
        }

        // adding without gaps
        public void AddLast(K key, V value, Func<int, int> hashGenerationFunction = null)
        {
            int lastIndex = Array.FindLastIndex(items, i => i.Value != null);
            int position = lastIndex == -1 ? 0 : lastIndex + 1;

            Entry<K, V> item = new Entry<K, V>()
            {
                Key = key,
                Value = value
            };

            items[position] = item;
        }
    }
}

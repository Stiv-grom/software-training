using System.Collections.Generic;

namespace HW5_LockFreeLinkedList
{
    internal class Node
    {
        private object data;
        private Node next;

        public Node(object nodeData, Node nextNode)
        {
            data = nodeData;
            next = nextNode;
        }

        public Node(object data) : this(data, null)
        {

        }

        public object Data { get => data; set => data = value; }
        public Node Next { get => next; set => next = value; }

        public override bool Equals(object obj)
        {
            var node = obj as Node;
            return node != null &&
                   EqualityComparer<object>.Default.Equals(data, node.data) &&
                   EqualityComparer<Node>.Default.Equals(next, node.next) &&
                   EqualityComparer<object>.Default.Equals(Data, node.Data) &&
                   EqualityComparer<Node>.Default.Equals(Next, node.Next);
        }

        public override int GetHashCode()
        {
            var hashCode = -942831942;
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(data);
            hashCode = hashCode * -1521134295 + EqualityComparer<Node>.Default.GetHashCode(next);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Data);
            hashCode = hashCode * -1521134295 + EqualityComparer<Node>.Default.GetHashCode(Next);
            return hashCode;
        }
    }
}

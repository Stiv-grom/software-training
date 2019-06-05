using System.Collections.Generic;
using System.Threading;

namespace HW5_LockFreeLinkedList
{
    internal class CustomLinkedList
    {
        volatile Node head;
        volatile Node addedNode;

        internal void AddFirst(object data)
        {
            Node addedNode = new Node(data);
            addedNode.Next = head;
            head = addedNode;
        }

        internal void AddFirstLockFree(object data)
        {
            addedNode = new Node(data);
            addedNode.Next = head;
            Interlocked.Exchange(ref head, addedNode);
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public override bool Equals(object obj)
        {
            var list = obj as CustomLinkedList;
            return list != null &&
                   EqualityComparer<Node>.Default.Equals(head, list.head);
        }

        public override int GetHashCode()
        {
            return -540810949 + EqualityComparer<Node>.Default.GetHashCode(head);
        }
    }
}

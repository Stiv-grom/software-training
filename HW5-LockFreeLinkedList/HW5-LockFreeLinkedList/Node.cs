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
        internal Node Next { get => next; set => next = value; }
    }
}

namespace HW5_LockFreeLinkedList
{
    internal class CustomLinkedList
    {
        private Node head;

        internal void InsertFront(object data)
        {
            Node addedNode = new Node(data);
            addedNode.Next = head;
            head = addedNode;
        }

        internal void InsertFrontLockFree(object data)
        {
            //todo
        }


        public bool IsEmpty()
        {
            return head == null;
        }
    }
}

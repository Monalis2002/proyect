namespace EcoDrive.Data
{
    public class LinkedListNode<T> where T : class
    {
        public T Data { get; set; }
        public LinkedListNode<T>? Next { get; set; }

        public LinkedListNode(T data)
        {
            Data = data;
            Next = null;
        }
    }
}

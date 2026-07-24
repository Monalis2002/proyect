using System;
using System.Collections.Generic;

namespace EcoDrive.Data
{
    public class DynamicLinkedList<T> where T : class
    {
        private LinkedListNode<T>? head;
        private int count;

        public int Count => count;
        public bool IsEmpty => head == null;

        public DynamicLinkedList()
        {
            head = null;
            count = 0;
        }

        public void AddFirst(T data)
        {
            var newNode = new LinkedListNode<T>(data);
            newNode.Next = head;
            head = newNode;
            count++;
        }

        public void AddLast(T data)
        {
            var newNode = new LinkedListNode<T>(data);
            
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                var current = head;
                while (current.Next != null)
                    current = current.Next;
                current.Next = newNode;
            }
            count++;
        }

        public T? RemoveFirst()
        {
            if (head == null)
                return null;

            T data = head.Data;
            head = head.Next;
            count--;
            return data;
        }

        public bool Remove(T data)
        {
            if (head == null)
                return false;

            if (head.Data == data)
            {
                head = head.Next;
                count--;
                return true;
            }

            var current = head;
            while (current.Next != null)
            {
                if (current.Next.Data == data)
                {
                    current.Next = current.Next.Next;
                    count--;
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public T[] ToArray()
        {
            var array = new T[count];
            var current = head;
            int index = 0;

            while (current != null)
            {
                array[index++] = current.Data;
                current = current.Next;
            }

            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public void Clear()
        {
            head = null;
            count = 0;
            GC.Collect();
        }

        public override string ToString()
        {
            return $"LinkedList<{typeof(T).Name}> - Count: {count}";
        }
    }
}

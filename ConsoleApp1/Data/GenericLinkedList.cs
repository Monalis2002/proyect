using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.Data
{
    public class GenericLinkedList<T>
    {
        private class Node
        {
            public T Data { get; set; }
            public Node? Next { get; set; }
            
            public Node(T data)
            {
                Data = data;
                Next = null;
            }
        }
        
        private Node? _head;
        public int Count { get; private set; }
        
        public GenericLinkedList()
        {
            _head = null;
            Count = 0;
        }
        
        public void Add(T value)
        {
            var newNode = new Node(value);
            
            if (_head == null)
            {
                _head = newNode;
            }
            else
            {
                var current = _head;
                while (current.Next != null)
                    current = current.Next;
                current.Next = newNode;
            }
            Count++;
        }
        
        public bool Remove(T value)
        {
            if (_head == null) return false;
            
            if (_head.Data!.Equals(value))
            {
                _head = _head.Next;
                Count--;
                return true;
            }
            
            var current = _head;
            while (current.Next != null)
            {
                if (current.Next.Data!.Equals(value))
                {
                    current.Next = current.Next.Next;
                    Count--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
        
        public T Get(int index)
        {
            if (index < 0 || index >= Count) 
                return default!;
            
            var current = _head;
            for (int i = 0; i < index; i++)
                current = current?.Next;
            
            if (current != null)
                return current.Data;
            return default!;
        }
        
        public void Clear()
        {
            _head = null;
            Count = 0;
        }
        
        public List<T> ToList()
        {
            var list = new List<T>();
            var current = _head;
            while (current != null)
            {
                list.Add(current.Data);
                current = current.Next;
            }
            return list;
        }
    }
}

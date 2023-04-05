using System.Collections.Generic;

namespace DesignCar.AppCode
{
    internal class Stack<T> where T : class
    {
        private readonly int _capacity;
        private readonly List<T> _items;

        public Stack(int maxCapacity = -1) 
        {
            if (maxCapacity < 0) maxCapacity = -1;
            _capacity = maxCapacity;
            _items = new List<T>();
        }

        public int Count()
        {
            return _items.Count;
        }

        public void Clear()
        {
            _items.Clear();
        }
        public void Push(T item)
        {
            _items.Add(item);
            while(_capacity != -1 && _items.Count > _capacity) 
            {
                _items.RemoveAt(0);
            }
        }
        public T Pop()
        {
            if (_items.Count > 0)
            {
                T temp = _items[_items.Count - 1];
                _items.RemoveAt(_items.Count - 1);
                return temp;
            }
            else
                return default(T);
        }
        public T Peek()
        {
            if (_items.Count > 0)
            {
                return _items[_items.Count - 1];
            }
            else
                return default(T);
        }
    }
}

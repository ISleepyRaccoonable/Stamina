using System;
using System.Collections.Generic;

namespace Assets.Project._Develop.Runtime.Utilities
{
    public class ReactiveList<T> where T : class
    {
        public event Action<int> IsAdded;
        public event Action IsRemoved;

        private List<T> _list = new List<T>();

        public IReadOnlyList<T> List => _list;

        public void Add(T component)
        {
            if (component == null)
                return;

            _list.Add(component);
            IsAdded?.Invoke(_list.Count);
        }

        public void Remove(T component)
        {
            if (_list.Contains(component) == false)
                return;
        
            _list.Remove(component);
            IsRemoved?.Invoke();
        }

        public void Clear()
        {
            _list.Clear();
        }
    }
}
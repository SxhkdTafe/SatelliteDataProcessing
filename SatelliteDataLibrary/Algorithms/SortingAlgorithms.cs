using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteDataLibrary
{
    public class SortingAlgorithms<T> where T : IComparable<T>
    {
        public LinkedList<T> Sorted {  get; private set; }
        private Func<LinkedList<T>, bool>? _sort;
        public SortingAlgorithms()
        {
            Sorted = new LinkedList<T>();
        }
        public enum SortType
        {
            Selection,
            Insertion
        }

        private bool SelectionSort(LinkedList<T> data)
        {
            if (data == null || data.First == null) return false;
            var outer = data.First;
            while (outer != null)
            {
                var min = outer;
                var inner = outer.Next;
                while (inner != null)
                {
                    if (inner.Value.CompareTo(min.Value) < 0)
                    {
                        min = inner;
                    }
                    inner = inner.Next;
                }
                if (!Object.ReferenceEquals(min, outer))
                {
                    T temp = outer.Value;
                    outer.Value = min.Value;
                    min.Value = temp;
                }
                outer = outer.Next;
            }
            Sorted = data; 
            return true;

        }
        private bool InsetionSort(LinkedList<T> data)
        {
            if (data == null || data.First == null) return false;

            var current = data.First.Next;

            while (current != null)
            {
                var next = current.Next; 
                var value = current.Value;
                var search = current.Previous;

                while (search != null && search.Value.CompareTo(current.Value) > 0)
                {
                    search = search.Previous;
                }

                
                if (search != current.Previous)
                {
                    data.Remove(current);
                    if (search == null)
                    {
                        data.AddFirst(value);
                    }
                    else
                    {

                        data.AddAfter(search, value);
                    }
                }
  

                current = next;
            }
            Sorted = data;
            return true;
        }
        public bool DataSort(LinkedList<T> data, SortType type)
        {
            _sort = type switch
            {
                SortType.Selection => SelectionSort,
                SortType.Insertion => InsetionSort,
                _ => throw new ArgumentException()
            };
            return _sort(data);
        }
    }
}

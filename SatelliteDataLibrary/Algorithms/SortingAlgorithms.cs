using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteDataLibrary
{
    public class SortingAlgorithms<T> where T : IComparable<T>
    {
        public LinkedList<T> Sorted {  get; set; }
        private Func<LinkedList<T>, bool> _sort;
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
            Sorted.Clear();
            Sorted = outer.List; 
            return true;

        }
        private bool InsetionSort(LinkedList<T> data)
        {
            if (data.First == null || data.First == null) return false;

            var current = data.First.Next;

            while (current != null)
            {
                var next = current.Next; 

                var search = current.Previous;

                while (search != null && search.Value.CompareTo(current.Value) > 0)
                {
                    search = search.Previous;
                }

                data.Remove(current);

                if (search == null)
                {
                    data.AddFirst(current);
                }
                else
                {

                    data.AddAfter(search, current);
                }

                current = next;
            }
            Sorted.Clear();
            Sorted = current.List;
            return true;
        }
        public void SelectSort(SortType type)
        {
            _sort = type switch
            {
                SortType.Selection => SelectionSort,
                SortType.Insertion => InsetionSort,
                _ => throw new ArgumentException()
            };
        }
        public bool DataSearch(LinkedList<T> data)
        {
            return _sort(data);
        }
    }
}

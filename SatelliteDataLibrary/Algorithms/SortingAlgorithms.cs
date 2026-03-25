using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteDataLibrary
{
    public class SortingAlgorithms<T> where T : IComparable<T>
    {
        private Func<LinkedList<T>, LinkedList<T>> _sort;
        public enum SortType
        {
            Selection,
            Insertion
        }
        private LinkedList<T> SelectionSort(LinkedList<T> data)
        {
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
            return data;

        }
        private LinkedList<T> InsetionSort(LinkedList<T> data)
        {
            if (data.First == null)
            {
                return null;
            }

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
            return data;
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
        public LinkedList<T> DataSearch(LinkedList<T> data)
        {
            return _sort(data);
        }
    }
}

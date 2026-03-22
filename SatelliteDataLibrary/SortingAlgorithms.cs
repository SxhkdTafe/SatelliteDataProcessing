using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteDataLibrary
{
    public class SortingAlgorithms<T> where T : IComparable<T>
    {
        private Func<T[], T[]> _sort;
        public enum SortType
        {
            Selection,
            Insertion
        }
        private T[] SelectionSort(T[] data)
        {
            int n  = data.GetLength(0);
            for (int i = 0; i < n - 1; i++)
            {
                int min = i;
                for (int j = i + 1;j < n +1; j++)
                {
                    if (data[j].CompareTo(data[min]) < 0)
                    {
                        min = j;
                    }
                }
                if (min != i)
                {
                    T temp = data[i];
                    data[i] = data[min];
                    data[min] = temp;
                }
            }
            return data;
        }
        private T[] InsetionSort(T[] data)
        {
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
        public T[] DataSearch(T[] data)
        {
            return _sort(data);
        }
    }
}

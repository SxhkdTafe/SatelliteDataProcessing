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
            int n  = data.Count;
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
        private LinkedList<T> InsetionSort(LinkedList<T> data)
        {
            for (int i =0; i < data.Count;i ++)
            {
                T tmp = data[i];
                int j = i - 1;
                while (j >= 0 && data[j].CompareTo(tmp) < 0)
                {
                    data[j + 1] = data[j];
                    j = j - 1;
                }
                data[j + 1] = tmp; 
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
        public T[] DataSearch(T[] data)
        {
            return _sort(data);
        }
    }
}

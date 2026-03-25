using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SatelliteDataLibrary
{
    public class SearchingAlgorithm<T> where T : IComparable<T>, IConvertible
    {
        private Func<LinkedList<T>, T, int, int, int> _search;
        
        public enum SearchType
        {
            Interative,
            Recursive
        }
        private int BinarySearchRecursive(LinkedList<T> data, T searchValue, int minIndex, int maxIndex)
        {
            if (data == null || data.First == null) return -1;
            if (minIndex > maxIndex)
            {    
                if (minIndex >= data.Count) minIndex = data.Count - 1;
                var nearest = data.First;
                for (int i = 0; i < minIndex; i++)
                    nearest = nearest.Next;
                return nearest.Value.ToInt32(null);
            }

            int midIndex = (minIndex + maxIndex) / 2;

            var current = data.First;
            for (int i = 0; i < midIndex; i++)
            {
                current = current.Next;
            }
            if (current.Value.CompareTo(searchValue) == 0)
            {
                return current.Value.ToInt32(null);
            }               
            else if (current.Value.CompareTo(searchValue) < 0)
            {
                return BinarySearchRecursive(data, searchValue, midIndex + 1, maxIndex);
            }               
            else
            {
                return BinarySearchRecursive(data, searchValue, minIndex, midIndex - 1);
            }            
        }
        private int BinarySearchInterative(LinkedList<T> data, T UsrInput, int minIndex, int maxIndex)
        {
            if (data == null || data.First == null ) return -1;
            if (minIndex > maxIndex)
            {
                if (minIndex >= data.Count) minIndex = data.Count - 1;
                var nearest = data.First;
                for (int i = 0; i < minIndex; i++)
                    nearest = nearest.Next;
                return nearest.Value.ToInt32(null);
            }

            while (minIndex <= maxIndex - 1)
            {
                int midIndex = (minIndex + maxIndex) / 2;

                var currentVal = data.First;
                for (int i = 0; i < midIndex; i++)
                {
                    currentVal = currentVal.Next;
                }
                if (currentVal.Value.CompareTo(UsrInput) == 0)
                {
                    return currentVal.Value.ToInt32(null);
                }
                else if (currentVal.Value.CompareTo(UsrInput) < 0)
                {
                    maxIndex = midIndex + 1;
                }
                else
                {
                    minIndex = midIndex + 1;
                }

            }
            return -1;
        }
        public void SelectSearch(SearchType type)
        {
            _search = type switch
            {
                SearchType.Interative => BinarySearchInterative,
                SearchType.Recursive => BinarySearchRecursive,
                _ => throw new ArgumentException()
            };
        }
        public int DataSearch(LinkedList<T> data, T UsrInput, int min, int max)
        {
           return _search(data, UsrInput, min, max);
        }
    }
}

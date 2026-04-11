using System;
using System.Buffers;
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
        private Func<LinkedList<T>, T, int, int, List<int>>? _search;
        private List<int> _results = new List<int>();
        private const double tol = 0.09;
        public enum SearchType
        {
            Interative,
            Recursive
        }
        private int ComparewithTol(T a, T b)
        {
            double da = Convert.ToDouble(a);
            double db = Convert.ToDouble(b);

            if (Math.Abs(da - db) <= tol) return 0;
            { 
                return da < db ? -1 : 1;
            }
        }
        private List<int> BinarySearchRecursive(LinkedList<T> data, T searchValue, int minIndex, int maxIndex)
        {

            if (minIndex > maxIndex) return _results;

            int midIndex = (minIndex + maxIndex) / 2;
            var current = data.First;
            
            for (int i = 0; i < midIndex; i++)
                current = current.Next;
            if (current == null) return _results;
            int cmp = ComparewithTol(current.Value, searchValue);

            if (cmp == 0)
            {
                _results.Add(midIndex);
                BinarySearchRecursive(data, searchValue, minIndex, midIndex - 1);
                BinarySearchRecursive(data, searchValue, midIndex + 1, maxIndex);
            }
            else if (cmp < 0)
            {
                BinarySearchRecursive(data, searchValue, midIndex + 1, maxIndex);
            }
            else
            {
                BinarySearchRecursive(data, searchValue, minIndex, midIndex - 1);
            }

            return _results;
        }


        private List<int> BinarySearchInterative(LinkedList<T> data, T UsrInput, int minIndex, int maxIndex)
        {
            var results = new List<int>();
            if (data == null || data.First == null) return results;

            while (minIndex <= maxIndex - 1)
            {
                int midIndex = (minIndex + maxIndex) / 2;

                var currentVal = data.First;
                for (int i = 0; i < midIndex; i++)
                {
                    currentVal = currentVal.Next;
                }
                if (currentVal == null) return _results;
                int cmp = ComparewithTol(currentVal.Value, UsrInput);
                if (cmp == 0)
                {
                    results.Add(midIndex);
                    minIndex = midIndex + 1;
                }
                else if (cmp < 0)
                {
                    minIndex = midIndex + 1;
                }
                else
                {
                    maxIndex = midIndex - 1;
                }

            }
            return results;
        }
        public List<int> DataSearch(LinkedList<T> data, T UsrInput, int min, int max, SearchType type)
        {
            _results = new List<int>();
            _search = type switch
            {
                SearchType.Interative => BinarySearchInterative,
                SearchType.Recursive => BinarySearchRecursive,
                _ => throw new ArgumentException()
            };
            return _search(data, UsrInput, min, max);
        }
    }
}

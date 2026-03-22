using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SatelliteDataLibrary
{
    public class SearchingAlgorithm<T> where T : IComparable<T>
    {
        private Func<T[], T, int> _search;
        
        public enum SearchType
        {
            Interative,
            Recursive
        }
        private int BinarySearchRecursive(T[] data, T UsrInput, int low, int high, int asc)
        {
            if (low > high) return -1; 
            int mid = (low + high) / 2;
            int cmp = asc * UsrInput.CompareTo(data[mid]);
            if (cmp == 0) return mid;
            else if (cmp > 0 ) return BinarySearchRecursive(data, UsrInput, mid + 1, high, asc);
            else return BinarySearchRecursive(data, UsrInput, low, mid - 1, asc);
        }
        private int BinarySearchInterative(T[] data, T UsrInput)
        {
            int low = 0;
            int max = data.GetLength(0) - 1;

            // Finds out if data is sorted asc or desc based upon if the initial val is smaller than the final val
            int asc = (data[low].CompareTo(data[max]) <= 0) ? 1 : -1;
            int result = -1;

            // Repeats until low is greater than max or max is less than low
            while (low <= max)
            {
                int mid = (low + max) / 2;

                // Sets middle value of current path 
                T midval = data[mid];

                // Finds if value matches and flips it if data is sorted desc and does not match
                int cmp = asc * UsrInput.CompareTo(midval);

                // If value is higher or lower than target then changes a var to indicate that
                int less = (cmp < 0 ? 1 : 0);
                int more = (cmp > 0 ? 1 : 0);

                // Increases of decreases min and max respectively by one depending upon if the val was higher or lower or sets it to the current value if the flag previous wasnt triggered 
                low = more * (mid + 1) + (1 - more) * low;
                max = less * (mid - 1) + (1 - less) * max;

                // Returns row index if found 
                if (cmp == 0)
                {
                    return mid;
                }
            }
            return result;
        }
        public void SelectSearch(SearchType type)
        {
            _search = type switch
            {
                SearchType.Interative => BinarySearchInterative,
                SearchType.Recursive => (data, item) =>
                {
                    int asc = (data[0].CompareTo(data[data.Length - 1]) <= 0 ? 1 : -1);
                    return BinarySearchRecursive(data, item, 0, data.Length - 1, asc);
                },
                _ => throw new ArgumentException()
            };
        }
        public int DataSearch(T[] data, T UsrInput)
        {
           return _search(data, UsrInput);
        }
    }
}

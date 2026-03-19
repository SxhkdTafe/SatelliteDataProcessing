using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SatelliteDataLibrary
{
    public class SearchingAlgorithm
    {
        private ISearchingAlgorithm _Select;

        public enum SearchType
        {
            Interative,
            Recursive
        }
        public int BinarySearchRecursive(Data[] data, int UsrInput)
        {
            return 0;
        }
        public int BinarySearchInterative(Data[] data, int UsrInput)
        {
            int low = 0;
            int max = data.GetLength(0) - 1;

            // Finds out if data is sorted asc or desc based upon if the initial val is smaller than the final val
            int asc = (data[low] <= data[max]) ? 1 : -1;
            int result = -1;

            // Repeats until low is greater than max or max is less than low
            while (low <= max)
            {
                int mid = (low + max) / 2;

                // Sets middle value of current path 
                int midval = data[mid];

                // Finds if value matches and flips it if data is sorted desc and does not match
                int cmp = asc * Math.Sign(UsrInput - midval);

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
            _Select = type switch
            {
                SearchType.Interative =>  BinarySearchInterative,
            }
        }


        public int DataSearch(Data[] data, int UsrInput)
        {
           return _Select.DataSearch(data, UsrInput);
        }
    }
}

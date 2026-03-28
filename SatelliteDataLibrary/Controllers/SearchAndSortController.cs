using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteDataLibrary.Controllers
{
    public class SearchAndSortController
    {
        private SearchingAlgorithm<double> SearchAlgorithms = new SearchingAlgorithm<double>();
        private SortingAlgorithms<double> SortAlgorithms = new SortingAlgorithms<double>();
        public List<double> SortedA { get; private set; } = new List<double>();
        public List<double> SortedB { get; private set; } = new List<double>();
        private bool sortFlagA = false;
        private bool sortFlagB = false;
        public enum SearchType { BinaryRecursive, BinaryIterative }
        public enum SortType { Selection, Insertion }
        public enum Dataset { A1, B2 }
        public bool IsSorted(Dataset dataset) => dataset == Dataset.A1 ? sortFlagA : sortFlagB;
        public List<double> SortData(LinkedList<double> unsorted, SortType sortType, Dataset dataset )
        {    
            var copy = new LinkedList<double>(unsorted);
            SortAlgorithms.DataSort(copy, sortType == SortType.Selection ? SortingAlgorithms<double>.SortType.Selection : SortingAlgorithms<double>.SortType.Insertion);
            var sorted = SortAlgorithms.Sorted.ToList();

            if (dataset == Dataset.A1) { sortFlagA = true; SortedA = sorted; }
            else { sortFlagB = true; SortedB = sorted; }

            return sorted;

        }
        public List<int> SearchData(LinkedList<double> sorted, double val, SearchType searchType, Dataset dataset)
        {
            var copy = new LinkedList<double>(sorted);
            
            if (dataset == Dataset.A1 && sortFlagA == false) return new List<int>();
            if (dataset == Dataset.B2 && sortFlagB == false) return new List<int>();

            return SearchAlgorithms.DataSearch(copy, val, 0, copy.Count, (searchType == SearchType.BinaryIterative ? SearchingAlgorithm<double>.SearchType.Interative : SearchingAlgorithm<double>.SearchType.Recursive));
        }
    }
}

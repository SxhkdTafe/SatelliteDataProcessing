using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteDataLibrary
{
    internal interface ISearchingAlgorithm
    {
        public int DataSearch(Data[] data, int target);
    }
}

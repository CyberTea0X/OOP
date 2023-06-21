using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    internal class SortArrayStrategy<T>: ISortingAlgorithm<T>
    {
        private ISortingAlgorithm<T> algorithm;

        public SortArrayStrategy()
        {
            this.algorithm = new QuickSortStrategy<T>();
        }

        public void Sort(ref T[] array)
        {
            algorithm.Sort(ref array);
        }
    }
}

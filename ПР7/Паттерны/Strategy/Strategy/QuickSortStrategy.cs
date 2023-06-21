using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    internal class QuickSortStrategy<T> : ISortingAlgorithm<T>
    {
        public void Sort(ref T[] array)
        {
            QuickSort(ref array, 0, array.Length - 1);
        }

        private void QuickSort(ref T[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(ref array, left, right);
                QuickSort(ref array, left, pivot - 1);
                QuickSort(ref array, pivot + 1, right);
            }
        }

        private int Partition(ref T[] array, int left, int right)
        {
            T pivot = array[right];
            int lowIndex = left - 1;
            for (int j = left; j < right; j++)
            {
                if (Comparer<T>.Default.Compare(array[j], pivot) <= 0)
                {
                    lowIndex++;
                    Swap(array, lowIndex, j);
                }
            }
            Swap(array, lowIndex + 1, right);
            return lowIndex + 1;
        }

        private void Swap(T[] array, int i, int j)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}

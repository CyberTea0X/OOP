using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    internal class BubbleSortStrategy<T> : ISortingAlgorithm<T>
    {
        public void Sort(ref T[] array)
        {
            bool flag = true;
            for (int i = 0; i < array.Length - 1 && flag; i++)
            {
                flag = false;
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (Comparer<T>.Default.Compare(array[j], array[j + 1]) > 0)
                    {
                        Swap(ref array, j, j + 1);
                        flag = true;
                    }
                }
            }
        }

        private void Swap(ref T[] array, int i, int j)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}

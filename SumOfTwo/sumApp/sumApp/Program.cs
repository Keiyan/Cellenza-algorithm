using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Workspace\\Algorithmie\\session 2\\SumOfTwo\\2sum.txt";
            string content = File.ReadAllText(path);
            string[] separtor = { "\r\n" };
            string[] tabStr = content.Split(separtor, StringSplitOptions.RemoveEmptyEntries);
            long[] tabLong = Array.ConvertAll(tabStr, s => long.Parse(s));
            Quicksort(tabLong, 0, tabLong.Length - 1);
            int indexLeft = -1;
            int indexRight = -1;
            long result = 0;
            long current = -1;
            bool mustAccept = true;
            for (int i = 0; i < tabLong.Length; i++)
            {
                current = tabLong[i];
                
                if (i == 0)
                {
                    Init(ref indexLeft, ref indexRight, tabLong, ref i);
                }
                else
                {
                    mustAccept = CalculIndex(ref indexLeft, ref indexRight, tabLong, i);
                }
                if (mustAccept)
                    result += Math.Abs(indexRight - indexLeft) + 1;
            }
            Console.WriteLine("Result :" + result);
            Console.ReadLine();
        }

        private static  bool HasRespectRules(long currentVal, long checkVal)
        {

            int limRight = 10000;
            int limLeft = -10000;

            return (currentVal + checkVal > limLeft && currentVal + checkVal < limRight);
        }

        private static int InitLeft(int indexLeft, long[] tabLong, int currentIndex)
        {
            for (int i = 1; i < tabLong.Length; i++)
            {
                if (HasRespectRules(tabLong[currentIndex], tabLong[i]))
                {
                    indexLeft = i;
                    break;
                }
                if (tabLong[currentIndex] + tabLong[i] > 10000)
                {
                    break;
                }
            }
            return indexLeft;
        }
        private static int InitRight(int indexRight, long[] tabLong, int currentIndex)
        {
            
            for (int i = tabLong.Length - 1; i > 0; i--)
            {
                if (HasRespectRules(tabLong[currentIndex], tabLong[i]))
                {
                    indexRight = i;
                    break;
                }
                if (tabLong[currentIndex] + tabLong[i] < -10000)
                {
                    break;
                }
            }
            return indexRight;
        }
        private static void Init(ref int indexLeft, ref int indexRight, long[] tabLong, ref int currentIndex)
        {
            
            
            while (currentIndex < tabLong.Length )
            {
                indexRight = InitRight(indexRight, tabLong, currentIndex);
                if (indexRight != -1)
                    break;
                currentIndex++;
            }

            while (currentIndex < tabLong.Length && indexLeft == -1)
            {
                indexLeft = InitLeft(indexLeft, tabLong, currentIndex);
                if (indexLeft != -1)
                    break;
            }
        }
        private static bool CalculIndex(ref int indexLeft, ref int indexRight, long[] tabLong, int currentIndex)
        {
            bool mustAccept = false;
            for (int i = indexLeft; i > currentIndex ; i--)
            {
                if (HasRespectRules(tabLong[currentIndex], tabLong[i]) && i > currentIndex)
                {
                    mustAccept = true;
                    indexLeft = i;
                    break;
                }
                if (tabLong[currentIndex] + tabLong[i] < -10000)
                {
                    break;
                }
            }
            for (int i = indexRight; i >= indexLeft && i > currentIndex; i++)
            {
                if (HasRespectRules(tabLong[currentIndex], tabLong[i]))
                {
                    mustAccept = true;
                    indexRight = i;
                    break;
                }
                if (tabLong[currentIndex] + tabLong[i] > 10000)
                {
                    break;
                }
            }
            return mustAccept;
        }

        public static void Quicksort(long[] elements, int left, int right)
        {
            int i = left, j = right;

            long pivot = elements[(left + right) / 2];




            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    long tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j);
            }

            if (i < right)
            {
                Quicksort(elements, i, right);
            }
        }
    }
}

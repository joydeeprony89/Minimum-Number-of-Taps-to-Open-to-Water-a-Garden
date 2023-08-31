using System;

namespace Minimum_Number_of_Taps_to_Open_to_Water_a_Garden
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] ranges = new int[] { 3, 4, 1, 1, 0, 0 };
            //int n = 5;
            //int[] ranges = new int[] { 0, 0, 0, 0 };
            //int n = 3;

            int[] ranges = new int[] { 1, 2, 1, 0, 2, 1, 0, 1 };
            int n = 7;

            Solution s = new Solution();
            var answer = s.MinTaps_(n, ranges);
            Console.WriteLine(answer);
        }
    }

    public class Solution
    {
        public int MinTaps(int n, int[] ranges)
        {
            // Step 1 - We have to generate the 1D array before solving the problem with the similar apparoach same as JUMP GAME 2
            // for creating the 1D array we will always take max bw starting index which is 0 and the formula which is given the area which a tap can cover
            // so lets say left side a tap can cover i - ranges[i], when i = 0, 0 - ranges[i] will always give negetove no, which is meaningless for us
            // as the start array index is 0, so we are taking the max of (0, left side formula), in case for an index i , i + ranges[i] give positive result
            // max of 0 and positive result will take as the starting position of the tap range from which it can cover
            // Basically 1D array will have answer at each position till which position a tap can max give water.
            // once we have the 1D array ready then will solve rest using Jump Game 2 problem, in which we try to find whether we can reach to the end of not kind of same
            // we are also trying to solve here as well. 
            int[] arr = new int[ranges.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                if (ranges[i] == 0) continue;
                int left = Math.Max(0, i - ranges[i]);
                arr[left] = Math.Max(arr[left], i + ranges[i]);
            }
            // Now solve rest we have to do JUMP Game 2 problem
            int l = 0; int r = 0; int count = 0;
            while (r < arr.Length - 1)
            {
                int maxJump = 0;
                for (int i = l; i < r + 1; i++)
                {
                    // Max index till which we can reach
                    maxJump = Math.Max(maxJump, arr[i]);
                }
                l = r + 1;
                r = maxJump;
                // boundary case, if at any position the jump is 0 which means we can not jump from that index, so we can return -1 as reaching end wont be possible
                if (l > r) return -1;
                count++;
            }

            return count;
        }


        // https://www.youtube.com/watch?v=Pk128gC_sdw
        public int MinTaps_(int n, int[] ranges)
        {
            int min = 0;
            int max = 0;
            int count = 0;
            int lastIndex = 0;
            while (max < n)
            {
                for (int i = 0; i < ranges.Length; i++)
                {
                    if (i >= lastIndex)
                    {
                        int left = i - ranges[i];
                        int right = i + ranges[i];
                        if (left <= min && right > max)
                        {
                            max = right;
                            lastIndex = i;
                        }
                    }
                }
                if (min == max) return -1;
                count++;
                min = max;
            }

            return count;
        }
    }
}

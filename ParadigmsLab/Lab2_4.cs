using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParadigmsLab
{
    public class Lab2_4
    {
        private List<int> array = new List<int>();
        private List<int> answer = new List<int>();

        public Lab2_4(List<int> input)
        {
            this.array = input;
        }

        public int[] findLowPeaks()
        {
            int i = 1;
            if (array[0] < array[i]) answer.Add(array[0]);
            for(; i < array.Count-2;i++)
            {
                if (array[i - 1] > array[i] && array[i + 1] > array[i])
                {
                    answer.Add(array[i]);
                }
            }
            if (array[i + 1] < array[i]) answer.Add(array[i + 1]);
            int count = answer.Count();
            answer.Add(count);
            return answer.ToArray<int>();
        }

    }
}
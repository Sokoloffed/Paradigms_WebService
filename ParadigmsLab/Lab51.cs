using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParadigmsLab
{
    public class Lab51
    {
        private List<int> array = new List<int>();
        List<int> answer = new List<int>();

        public Lab51(List<int> input)
        {
            this.array = input;
        }

        public int[] findBelowZero()
        {
            for(int i = 0; i < array.Count; i++)
            {
                if(array.ElementAt(i) < 0)
                {
                    answer.Add(i);
                }
            }
            int count = answer.Count();
            answer.Add(count);
            return answer.ToArray<int>();

        }
     
    }
}
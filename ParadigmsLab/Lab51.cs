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
        int amount;

        public Lab51(List<int> input)
        {
            this.array = input;
            this.amount = 0;
        }

        public void findAnswer()
        {
            for(int i = 0; i < array.Count; i++)
            {
                if(array.ElementAt(i) < 0)
                {
                    answer.Add(i);
                    amount++;
                }
            }
        }
        
        public int[] getArray()
        {
            return answer.ToArray<int>();
        }

        public int getAmount()
        {
            return amount;
        }



    }
}
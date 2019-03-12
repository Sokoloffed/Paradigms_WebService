using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParadigmsLab
{
    public class Lab22
    {
        List<int> array = new List<int>();
        HashSet<int> answer = new HashSet<int>();

        public Lab22(List<int> input)
        {
            this.array = input;
        }

        public void createArray()
        {
            for (int i = 0; i < this.array.Count; i++)
            {
                int counter = 0;
                for (int k = 0; k < this.array.Count; k++)
                {
                    if (array[k] == array[i]) counter++;
                }
                if(counter == 3)
                {
                    answer.Add(array[i]);
                }
            }
        }


        public int[] findThreefoldElements()
        {
            createArray();
            List<int> lst = new List<int>();
            foreach(int hs in answer)
            {
                lst.Add(hs);
            }
            return lst.ToArray<int>();
        }
    }
}
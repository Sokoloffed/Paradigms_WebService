using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ParadigmsLab
{
    public class Lab3_7
    {
        public string[] automatonLines;
        public int initialState;
        public int amountOfStates;
        public int amountOfChars;
        public int[] finalStates;
        public int[,] matrixOfTransitions;

        public Lab3_7()
        {
            if (!File.Exists("C:\automaton2.txt"))
            {
                automatonLines = File.ReadAllLines("C:\\automaton2.txt");
            }
            DoPreparations();

        }

        public void DoPreparations()
        {
            initialState = Convert.ToInt32(automatonLines[0].ElementAt(0)) - 48;
            amountOfStates = Convert.ToInt32(automatonLines[1].ElementAt(0)) - 48;
            amountOfChars = Convert.ToInt32(automatonLines[2].ElementAt(0)) - 48;
            string trim = Regex.Replace(automatonLines[3], @"s", "");
            finalStates = new int[trim.ElementAt(0)];
            for(int i = 1; i < trim.Length; i++)
            {
                finalStates[i - 1] = trim.ElementAt(i);
            }
            matrixOfTransitions = new int[amountOfStates, amountOfChars];

            for(int i = 4; i < automatonLines.Length; i++)
            {
                string trimLine = automatonLines[i].Replace(" ", string.Empty);

            }


        }

    }
}
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
        public string w;
        public List<List<List<int>>> allPossibleW;
        public string alphabet = "abcdefghijklmnopqrstuvwxyz";
        public string subAlphabet;

        public Lab3_7(string w_word)
        {
            if (!File.Exists("C:\automaton2.txt"))
            {
                automatonLines = File.ReadAllLines("C:\\automaton2.txt");
            }
            this.w = w_word;
            DoPreparations();

        }

        public void DoPreparations()
        {
            initialState = Convert.ToInt32(automatonLines[0].ElementAt(0)) - 48;
            amountOfStates = Convert.ToInt32(automatonLines[1].ElementAt(0)) - 48;
            amountOfChars = Convert.ToInt32(automatonLines[2].ElementAt(0)) - 48;
            string trim = Regex.Replace(automatonLines[3], @"s", "");
            finalStates = new int[trim.ElementAt(0)];
            subAlphabet = alphabet.Substring(0, amountOfChars);
            for(int i = 1; i < trim.Length; i++)
            {
                finalStates[i - 1] = trim.ElementAt(i);
            }
            matrixOfTransitions = new int[amountOfStates, amountOfChars];
            for (int i = 0; i < amountOfStates; i++)
            {
                for (int j = 0; j < amountOfChars; j++)
                {
                    matrixOfTransitions[i, j] = -2;
                }
            }
            for(int i = 4; i < automatonLines.Length - 1; i++)
            {
                string trimLine = automatonLines[i].Replace(" ", string.Empty);
                int asciiChar = subAlphabet.IndexOf(trimLine.ElementAt(1));
                int prevState = Convert.ToInt32(trimLine.ElementAt(0)) - 48;
                int nextState = Convert.ToInt32(trimLine.ElementAt(2)) - 48;
                if (prevState == nextState)
                    matrixOfTransitions[prevState, asciiChar] = -1;
                else matrixOfTransitions[prevState, asciiChar] = nextState;
            }
            SetAllPossibleW();
        }

        public void SetAllPossibleW()
        {
            this.allPossibleW = GetAllW(this.w);
        }

        public int[,] GetMatrixOfTransitions()
        {
            return this.matrixOfTransitions;
        }

        public List<List<int>> GetAllPairsForLetter(int startState, int finalState, int letter)
        {
            List<List<int>> allPairs = new List<List<int>>();
            for (int i = startState; i < finalState; i++)
            {
                List<int> pair = new List<int>();
                if (matrixOfTransitions[i, letter] == -2)
                    continue;
                else if (matrixOfTransitions[i, letter] == -1)
                {
                    pair.Add(i);
                    pair.Add(i);
                    allPairs.Add(pair);
                }
                else
                {
                    pair.Add(i);
                    pair.Add(matrixOfTransitions[i, letter]);
                    allPairs.Add(pair);
                }
            }
            return allPairs;
        }

        public List<List<List<int>>> GetAllW(string word)
        {
            char[] wAsChar = word.ToCharArray();
            int[] wAsInt = new int[wAsChar.Length];
            for(int i = 0; i < wAsChar.Length; i++)
            {
                wAsInt[i] = subAlphabet.IndexOf(word);
            };
            List<List<List<int>>> listOfAllPairsForLetters = new List<List<List<int>>>();
            for (int i = 0; i < wAsInt.Length; i++)
            {
                int letter = wAsInt[i];

                List<List<int>> possiblePairsForLetter = GetAllPairsForLetter(0, amountOfStates, letter);
                listOfAllPairsForLetters.Add(possiblePairsForLetter);
            }
            return listOfAllPairsForLetters;
        }



    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using ParadigmsLab;

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
        public List<int> wordd;
        public List<List<int>> testRecursion;

        public Lab3_7(string w_word)
        {
            Lab45 lab = new Lab45();

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
            SetPossibleWRecursive();
            /*if(allPossibleW.Count > 1)
            {
                List<int> wordW = new List<int>();
                int i = 0;
                int j = 0;
                int k = 0;
                bool exit = false;
                for(i = 0; i < allPossibleW.Count - 1; i++)
                {
                    exit = false;
                    for(j = 0; j < allPossibleW.ElementAt(i).Count; j++)
                    {
                        for (k = 0; k < allPossibleW.ElementAt(i + 1).Count; k++)
                        {
                            if (allPossibleW.ElementAt(i).ElementAt(j).ElementAt(1) == allPossibleW.ElementAt(i + 1).ElementAt(k).ElementAt(0))
                            {
                                wordW.Add(allPossibleW.ElementAt(i).ElementAt(j).ElementAt(0));
                                wordW.Add(allPossibleW.ElementAt(i).ElementAt(j).ElementAt(1));
                                wordW.Add(allPossibleW.ElementAt(i + 1).ElementAt(k).ElementAt(0));
                                wordW.Add(allPossibleW.ElementAt(i + 1).ElementAt(k).ElementAt(1));
                                exit = true;
                                break;
                            }
                        }
                        if (!exit) break;
                    }
                }
                this.wordd = wordW;
            }*/

            List<int> answer = ConnectStates(0, 3);
        }


        public void SetPossibleWRecursive()
        {
            testRecursion = new List<List<int>>();
            foreach (List<int> l in allPossibleW.ElementAt(allPossibleW.Count - 1))
            {
                RecursiveWordW(l.ElementAt(0), l.ElementAt(1));
            }
        }

        public void RecursiveWordW(int firstPairMember, int secondPairMember)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < amountOfStates && i != firstPairMember; i++)
            {
                for (int j = 0; j < amountOfChars; j++)
                {
                    if (matrixOfTransitions[i,j] == firstPairMember)
                    {
                        list.Add(i);
                        list.Add(firstPairMember);
                        this.wordd = list;
                        RecursiveWordW(i, firstPairMember);

                    }
                }
            }
        }

        public List<int> ConnectStates(int firstS, int secondS)
        {
            List<int> answer = new List<int>();

            List<int> reachableFromFirst = new List<int>();
            for(int i = 0; i < amountOfChars; i++)
            {
                if (matrixOfTransitions[firstS, i] == -2)
                    continue;
                else if (matrixOfTransitions[firstS, i] == -1)
                    continue;
                else reachableFromFirst.Add(matrixOfTransitions[firstS, i]);
            }

            List<int> priorForSecond = new List<int>();
            for(int i = 0; i < amountOfStates; i++)
            {
                for(int j = 0; j < amountOfChars; j++)
                {
                    if (matrixOfTransitions[i, j] == secondS)
                        priorForSecond.Add(i);

                }
            }

            List<int> intersected = new List<int>();

            for(int i = 0; i < priorForSecond.Count; i++)
            {
                for(int j = 0; j < reachableFromFirst.Count; j++)
                {
                    if(priorForSecond.ElementAt(i) == reachableFromFirst.ElementAt(j))
                    {
                        intersected.Add(priorForSecond.ElementAt(i));
                    }
                }
            }

            if(intersected.Count != 0)
            {
                answer.Add(firstS);
                for(int i = 0; i < intersected.Count; i++)
                {
                    answer.Add(intersected.ElementAt(i));
                }
                answer.Add(secondS);

            }



            return null;
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

                List<List<int>> possiblePairsForLetter = GetAllPairsForLetter(0, amountOfStates-1, letter);
                listOfAllPairsForLetters.Add(possiblePairsForLetter);
            }
            return listOfAllPairsForLetters;
        }

        public void FindConnectionBetweenStates(int startState, int finState)
        {
            List<List<int>> possibleMiddleLetters = new List<List<int>>();
            bool exit = false;
            HashSet<int> firstStatesArr = new HashSet<int>();
            firstStatesArr.Add(startState);
            while (!exit)
            {
                for(int i = 0; i < firstStatesArr.Count; i++)
                {
                    if (exit) break;
                    List<int> pair = new List<int>();
                    if(matrixOfTransitions[startState,i] == finState)
                    {
                        exit = true;
                        pair.Add(startState);
                        pair.Add(finState);
                        break;
                    }
                    else
                    {
                        //if(matrixOfTransitions[])
                    }

                }
            }
        }



    }
}
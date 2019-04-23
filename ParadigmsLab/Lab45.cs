using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ParadigmsLab
{
    public class Lab45
    {
        public string[] KFGrammar;
        public string terminals;
        public string nonterminals;
        public string epsilonNonterminals;
        public int amountTerminals;
        public int amountNonterminals;
        public string[] rules;
        List<string> rulesWithoutEpsilon;
        Dictionary<char, List<char>> SForAll;
        List<char> rightRecursive;
        bool flag;

        public Lab45()
        {
            if (File.Exists("C:\\Users\\admin\\Documents\\grammar.txt"))

            {
                KFGrammar = File.ReadAllLines("C:\\Users\\admin\\Documents\\grammar.txt");
            }

            string trimTerm = Regex.Replace(KFGrammar[0], @"\s", "");
            amountTerminals = trimTerm.ElementAt(0) - 48;
            terminals = "";
            for(int i = 1; i < trimTerm.Length; i++)
            {
                terminals += trimTerm.ElementAt(i);
            }

            string trimNonterm = Regex.Replace(KFGrammar[1], @"\s", "");
            amountNonterminals = trimNonterm.ElementAt(0) - 48;
            nonterminals = "";
            for(int i = 1; i < trimNonterm.Length; i++)
            {
                nonterminals += trimNonterm.ElementAt(i);
            }

            rules = new string[KFGrammar.Length - 2];
            for(int i = 0; i < rules.Length; i++)
            {
                string trim = Regex.Replace(KFGrammar[i + 2], @"->", "");
                rules[i] = trim;
            }

            epsilonNonterminals = "";
            List<int> epsilonIndicies = new List<int>();
            for(int i = 0; i < rules.Length; i++)
            {
                if (rules[i].ElementAt(1) == 'e')
                {
                    epsilonNonterminals += rules[i].ElementAt(0);
                    epsilonIndicies.Add(i);
                }
            }

            rulesWithoutEpsilon = new List<string>();
            for(int i = 0; i < rules.Length; i++)
            {
                if (epsilonIndicies.Contains(i))
                    continue;
                else rulesWithoutEpsilon.Add(rules[i]);                   
            }

            string trimN = Regex.Replace(nonterminals, @"s", "");

            this.nonterminals = trimN;

            rightRecursive = new List<char>();
            SForAll = new Dictionary<char, List<char>>();
            for(int i = 0; i < nonterminals.Length; i++)
            {
                flag = false;
                SForAll.Add(nonterminals[i], new List<char>());
                FindIfRightRecursive(nonterminals[i]);
                if (flag)
                {
                    rightRecursive.Add(nonterminals[i]);
                }
            }

           // ReturnRightRecursive();

            
        }

        public void FindIfRightRecursive(char nonterm)
        {
            int initialCount = SForAll[nonterm].Count;
            SForAll[nonterm].Add(nonterm);
            foreach(char nont in SForAll[nonterm].ToArray())
            {
                SForAll[nonterm].Remove(nonterm);
                for(int j = 0; j < rulesWithoutEpsilon.Count; j++)
                {
                    string rule = rulesWithoutEpsilon.ElementAt(j);
                    if (rule[0] == nont)
                    {
                        for (int k = rule.Length - 1; k > 0; k--)
                        {
                            if (epsilonNonterminals.Contains(rule[k]))
                                continue;
                            else
                            {
                                if (!SForAll[nonterm].Contains(rule[k]))
                                {
                                    if (rule[k] == nonterm)
                                        flag = true;
                                    SForAll[nonterm].Add(rule[k]);

                                }
                                break;

                            }
                        }
                    }
                    else continue;
                }                      
            }    
            //List<char> noDuplicates = SForAll[nonterm].Distinct().ToList();
            SForAll[nonterm].Distinct();
            int finalCount = SForAll[nonterm].Count;
            
            if (finalCount > initialCount)
                FindIfRightRecursive(nonterm);
            if (flag)
            {
                return;
            }
        }

        public List<char> GetAllRightRecursiveNonterminals()
        {
            return this.rightRecursive;
        }

    }
}
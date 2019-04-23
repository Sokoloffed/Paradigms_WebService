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

            FindIfRightRecursive();

            ReturnRightRecursive();

            
        }

        public void ReturnRightRecursive()
        {
            rightRecursive = new List<char>();
            for(int i = 0; i < SForAll.Count; i++)
            {
                if (SForAll.ElementAt(i).Value.Contains(SForAll.ElementAt(i).Key))
                    rightRecursive.Add(SForAll.ElementAt(i).Key);
            }
        }

        public void FindIfRightRecursive()
        {
            SForAll = new Dictionary<char, List<char>>();
            for (int i = 0; i < nonterminals.Length; i++)
            {
                char nonterm = nonterminals.ElementAt(i);
                List<char> S = new List<char>();
                for(int j = 0; j < rulesWithoutEpsilon.Count; j++)
                {
                    string rule = rulesWithoutEpsilon.ElementAt(j);
                    if (rule[0] == nonterm)
                    {
                        for(int k = rule.Length - 1; k > 0; k--)
                        {
                            if (epsilonNonterminals.Contains(rule[k]))
                                continue;
                            else
                            {
                                S.Add(rule[k]);
                                break;
                            }
                        }
                    }
                    else continue;
                }
                SForAll.Add(nonterm, S);
            }
        }

    }
}
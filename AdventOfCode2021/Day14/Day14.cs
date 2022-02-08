using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode2021
{
    class Day14
    {
        public static Dictionary<string, string> rules = new Dictionary<string, string>();
        public static void StartDay14()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day14\input.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                var lines = prepareInput(ReaderObject);
                //string template = "NNCB";
                string template = "HBCHSNFFVOBNOFHFOBNO";
                Console.WriteLine("Part 1: " + insertion(template, 10));
                Console.WriteLine("Part 2: " + insertion(template, 40));
            }
        }

        private static List<string> prepareInput(StreamReader ReaderObject)
        {
            string line;
            List<string> lines = new List<string>();
            while ((line = ReaderObject.ReadLine()) != null)
            {
                lines.Add(line);
            }
            rules = lines.Select(l => l.Split(" -> ")).ToDictionary(parts => parts[0], parts => parts[1]);
            return lines;
        }

        private static long insertion(string template, int steps)
        {
            var pairDictionary = getPairs(template);
            var letterDictionary = getLetters(template);

            for (var i = 0; i < steps; i++)
            {
                pairDictionary = runRules(pairDictionary, letterDictionary);
            }

            var max = letterDictionary.Max(x => x.Value);
            var min = letterDictionary.Min(x => x.Value);
            return max - min;
        }


        private static Dictionary<string, long> runRules(Dictionary<string, long> pairDictionary, Dictionary<char, long> letterDictionary)
        {
            Dictionary<string, long> newPairs = new Dictionary<string, long>();
            foreach (var (pair, i) in pairDictionary)
            {
                string insertion = rules[pair];
                letterDictionary[insertion[0]] = letterDictionary.GetValueOrDefault(insertion.ToCharArray()[0], 0) + i;
                
                var rule = pair[0] + insertion;
                newPairs[rule] = newPairs.GetValueOrDefault(rule, 0) + i;

                var ruleInsertion = insertion + pair[1];
                newPairs[ruleInsertion] = newPairs.GetValueOrDefault(ruleInsertion, 0) + i;
            }
            return newPairs;
        }

        private static Dictionary<string, long> getPairs(string template)
        {
            Dictionary<string, long> pairDictionary = new Dictionary<string, long>();
            for (var i = 0; i < template.Length - 1; i++)
            {
                var pair = template[i..(i + 2)];
                pairDictionary[pair] = pairDictionary.GetValueOrDefault(pair, 0) + 1;
            }
            return pairDictionary;
        }

        private static Dictionary<char, long> getLetters(string template)
        {
            Dictionary<char, long> letterDictionary = new Dictionary<char, long>();
            for (var i = 0; i < template.Length; i++)
            {
                letterDictionary[template[i]] = letterDictionary.GetValueOrDefault(template[i], 0) + 1;
            }
            return letterDictionary;
        }
    }
}

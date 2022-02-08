using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2021
{
    class Day08
    {
        public static void StartDay08()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day08\input.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                string[] digits = prepareDigits();
                List<string[]> input = prepareInput(ReaderObject);
                Console.WriteLine("Part 1: " + countUniqueDigits(digits, input));
                Console.WriteLine("Part 2: " + decodeSignals(input));
            }
        }

        private static string[] prepareDigits()
        {
            string[] digits = new string[10] { "abcefg", "cf", "acdeg", "acdfg", "bcdf", "abdfg", "abdefg", "acf", "abcdefg", "abcdfg" };
            return digits;
        }

        private static List<string[]> prepareInput(StreamReader ReaderObject)
        {
            string line;
            List<string[]> input = new List<string[]>();
            while ((line = ReaderObject.ReadLine()) != null)
            {
                string[] splittedLine = line.Split(" | ");
                string[] i = splittedLine[0].Split(" ");
                string[] o = splittedLine[1].Split(" ");
                input.Add(i.Concat(o).ToArray());
            }
            return input;
        }

        private static int countUniqueDigits(string[] digits, List<string[]> outputLines)
        {
            int counter = 0;
            foreach(var output in outputLines)
            {
                counter += output.Skip(10).Where(i =>
                i.Length == digits[1].Length ||
                i.Length == digits[4].Length ||
                i.Length == digits[7].Length ||
                i.Length == digits[8].Length).ToList().Count;
            }
            return counter;
        }

        private static int decodeSignals(List<string[]> inputLines)
        {
            int result = 0;
            foreach (var input in inputLines)
            {
                result += Int32.Parse(decodeDigits(input));
            }
            return result;
        }

        private static string decodeDigits(string[] inputs)
        {
            string[] decodedDigits = new string[10];
            decodedDigits[1] = inputs.Where(input => input.Length == 2).First();
            decodedDigits[4] = inputs.Where(input => input.Length == 4).First();
            decodedDigits[7] = inputs.Where(input => input.Length == 3).First();
            decodedDigits[8] = inputs.Where(input => input.Length == 7).First();
            // a,b,c,d,e,f,g
            string[] wires = new string[7];

            //uniques: 1, 4, 7, 8

            // a
            wires[0] = decodedDigits[1].Except(decodedDigits[7]).Union(decodedDigits[7].Except(decodedDigits[1])).First().ToString();

            // 9
            var a4 = decodedDigits[4] + wires[0];
            decodedDigits[9] = inputs.Where(input =>
                input.Length == 6 &&
                input.Contains(a4[0]) &&
                input.Contains(a4[1]) &&
                input.Contains(a4[2]) &&
                input.Contains(a4[3]) &&
                input.Contains(a4[4])).First();

            // g
            wires[6] = a4.Except(decodedDigits[9]).Union(decodedDigits[9].Except(a4)).First().ToString();

            // e
            wires[4] = decodedDigits[9].Except(decodedDigits[8]).Union(decodedDigits[8].Except(decodedDigits[9])).First().ToString();

            // 3
            var ag1 = decodedDigits[1] + wires[0] + wires[6];
            decodedDigits[3] = inputs.Where(input =>
                input.Length == 5 &&
                input.Contains(ag1[0]) &&
                input.Contains(ag1[1]) &&
                input.Contains(ag1[2]) &&
                input.Contains(ag1[3])).First();

            // d
            wires[3] = ag1.Except(decodedDigits[3]).Union(decodedDigits[3].Except(ag1)).First().ToString();

            // 0
            decodedDigits[0] = decodedDigits[8].Replace(wires[3], "");

            // b
            wires[1] = decodedDigits[4].Replace(wires[3], "").Replace(decodedDigits[1][0].ToString(), "").Replace(decodedDigits[1][1].ToString(), "");

            // 6
            decodedDigits[6] = inputs.Where(input =>
                input.Length == 6 &&
                String.Concat(input.OrderBy(c => c)) != String.Concat(decodedDigits[0].OrderBy(c => c)) &&
                String.Concat(input.OrderBy(c => c)) != String.Concat(decodedDigits[9].OrderBy(c => c))).First();

            // 5
            decodedDigits[5] = decodedDigits[6].Replace(wires[4], "");

            // c
            wires[2] = decodedDigits[5].Except(decodedDigits[9]).Union(decodedDigits[9].Except(decodedDigits[5])).First().ToString();

            // f
            wires[5] = decodedDigits[1].Replace(wires[2], "");

            // 2
            decodedDigits[2] = wires[0] + wires[2] + wires[3] + wires[4] + wires[6];

            // sortDigits
            decodedDigits = decodedDigits.Select(digit => String.Concat(digit.OrderBy(c => c))).ToArray();

            string[] outputs = inputs.Skip(10).ToArray();
            string decodedOutput = "";
            for (var i = 0; i < outputs.Length; i++)
            {
                string sortedOutput = String.Concat(outputs[i].OrderBy(c => c));
                decodedOutput = decodedOutput + Array.IndexOf(decodedDigits, sortedOutput).ToString();
            }

            return decodedOutput;
        }
    }
}

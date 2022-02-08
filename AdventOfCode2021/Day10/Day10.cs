using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode2021
{
    class Day10
    {
        public static void StartDay10()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day10\input.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                List<string> inputLines = prepareInput(ReaderObject);
                Console.WriteLine("Part 1: " + countCurruptedChunkPoints(inputLines));
            }
        }

        private static List<string> prepareInput(StreamReader ReaderObject)
        {
            string line;
            List<string> input = new List<string>();
            while ((line = ReaderObject.ReadLine()) != null)
            {
                input.Add(line);
            }
            return input;
        }

        private static double countCurruptedChunkPoints(List<string> inputLines)
        {
            int[] points = new int[] { 1197, 3, 57, 25137 };
            int[] points_Part2 = new int[] { 3, 1, 2, 4 };
            string[] openingBrackets = new string[] { "{", "(", "[", "<" };
            string[] closingBrackets = new string[] { "}", ")", "]", ">" };
            Stack chunk = new Stack();
            double result = 0;
            List<double> scores = new List<double>();
            foreach (var line in inputLines)
            {
                bool corrupted = false;
                foreach (char c in line)
                {
                    if (openingBrackets.Contains(c.ToString()))
                    {
                        chunk.Push(c.ToString());
                    }
                    else if (closingBrackets.Contains(c.ToString()) && chunk.Count > 0)
                    {
                        var popped = chunk.Pop();
                        var indexClosing = Array.IndexOf(closingBrackets, c.ToString());

                        if (popped.ToString() != openingBrackets[indexClosing])
                        {
                            var indexPoppedClosing = Array.IndexOf(openingBrackets, popped.ToString());
                            //Console.WriteLine("Expected " + closingBrackets[indexPoppedClosing] + ", but found " + c.ToString() + " instead");
                            result += points[indexClosing];
                            corrupted = true;
                            break;
                        }
                    }
                }
                // Part 2
                double result_Part2 = 0;
                if (!corrupted)
                {
                    foreach (var bracket in chunk)
                    {
                        var index = Array.IndexOf(openingBrackets, bracket.ToString());
                        result_Part2 *= 5;
                        result_Part2 += points_Part2[index];
                    }
                    scores.Add(result_Part2);
                }
                chunk.Clear();
            }
            scores.Sort();
            Console.WriteLine("Part 2: " + scores[scores.Count / 2]);
            return result;
        }

    }
}

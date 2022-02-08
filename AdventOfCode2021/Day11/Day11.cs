using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode2021
{
    class Day11
    {
        public static int res = 0;
        public static List<string> alreadyFlashed = new List<string>();

        public static void StartDay11()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day11\input.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                List<int[]> inputLines = prepareInput(ReaderObject);
                //Console.WriteLine("Part 1: " + flashOctopus(inputLines, 100));
                Console.WriteLine("Part 2: " + flashOctopus(inputLines));
            }
        }

        private static List<int[]> prepareInput(StreamReader ReaderObject)
        {
            string line;
            List<int[]> input = new List<int[]>();
            while ((line = ReaderObject.ReadLine()) != null)
            {
                int[] nums = new int[line.Length];
                for (var i = 0; i < line.Length; i++)
                {
                    var num = Int32.Parse(line[i].ToString());
                    nums[i] = num;
                }
                input.Add(nums);

            }
            return input;
        }

        private static int flashOctopus(List<int[]> input, double steps)
        {
            for (var j = 0; j < steps; j++)
            {
                for (var k = 0; k < input.Count; k++)
                {
                    for (var i = 0; i < input[k].Length; i++)
                    {
                        flash(input, k, i);
                    }
                }
                alreadyFlashed.Clear();
            }
            return res;
        }

        private static double flashOctopus(List<int[]> input)
        {
            double j = 0;
            while(true)
            {
                for (var k = 0; k < input.Count; k++)
                {
                    for (var i = 0; i < input[k].Length; i++)
                    {
                        flash(input, k, i);
                    }
                }
                alreadyFlashed.Clear();
                if (res == input.Count * input[0].Length) break;
                else res = 0;
                j++;
            }
            return j+1;
        }

        private static void flash(List<int[]> input, int k, int i)
        {
            if (k >= 0 && k < input.Count)
            {
                if (i >= 0 && i < input[k].Length)
                {
                    if (!alreadyFlashed.Contains(k + "" + i))
                    {
                        input[k][i]++;
                        if (input[k][i] >= 10 && !alreadyFlashed.Contains(k + "" + i))
                        {
                            alreadyFlashed.Add(k + "" + i);
                            res++;
                            input[k][i] = 0;
                            flash(input, k, i - 1);
                            flash(input, k, i + 1);
                            flash(input, k - 1, i);
                            flash(input, k - 1, i - 1);
                            flash(input, k - 1, i + 1);
                            flash(input, k + 1, i);
                            flash(input, k + 1, i - 1);
                            flash(input, k + 1, i + 1);
                        }
                    }
                }
            }
        }
    }
}

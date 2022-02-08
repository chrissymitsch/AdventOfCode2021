using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    class Day03
    {
        public static void StartDay03()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day03\input.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                string line;
                List<char[]> lines = new List<char[]>();
                int length = 0;
                while (!String.IsNullOrEmpty(line = ReaderObject.ReadLine()))
                {
                    char[] bits = line.ToCharArray();
                    length = bits.Length;
                    lines.Add(bits);
                }
                Console.WriteLine("Part 1: " + getGammaEpsilon(lines, length));
                Console.WriteLine("Part 2: " + getOxygenCO2(lines, length));
            }
        }

        private static int getOxygenCO2(List<char[]> lines, int length)
        {
            List<char[]> filtered = lines;
            var oxygen = 0;
            var co2 = 0;

            for (var i = 0; i < length; i++)
            {
                var mostAndLeast = getMostAndLeastOfPosition(filtered, i);
                filtered = filtered.Where(line => (line[i] - '0') == mostAndLeast[0]).ToList();
                if (filtered.Count == 1)
                {
                    oxygen = Convert.ToInt32(String.Join("", filtered[0]), 2);
                    break;
                }
            }

            filtered = lines;

            for (var i = 0; i < length; i++)
            {
                var mostAndLeast = getMostAndLeastOfPosition(filtered, i);
                filtered = filtered.Where(line => (line[i] - '0') == mostAndLeast[1]).ToList();
                if (filtered.Count == 1)
                {
                    co2 = Convert.ToInt32(String.Join("", filtered[0]), 2);
                    break;
                }
            }

            return oxygen * co2;
        }


        private static int getGammaEpsilon(List<char[]> lines, int length)
        {
            int[] mostCommonBits = new int[length];
            int[] leastCommonBits = new int[length];
            for (var i = 0; i < length; i++)
            {
                var mostAndLeast = getMostAndLeastOfPosition(lines, i);
                mostCommonBits[i] = mostAndLeast[0];
                leastCommonBits[i] = mostAndLeast[1];
            }

            int mostCommonBit = Convert.ToInt32(String.Join("", mostCommonBits), 2);
            int leastCommonBit = Convert.ToInt32(String.Join("", leastCommonBits), 2);
            return mostCommonBit * leastCommonBit;
        }

        private static List<int> getMostAndLeastOfPosition(List<char[]> lines, int position)
        {
            int mostOrLeast = 0;
            int mostCommonBit;
            int leastCommonBit;

            for (var i = 0; i < lines.Count; i++)
            {
                var bits = lines[i];
                int bit = bits[position] - '0';
                mostOrLeast += bit;
            }

            float half = lines.Count / 2f;
            if (mostOrLeast >= Math.Ceiling(half))
            {
                mostCommonBit = 1;
                leastCommonBit = 0;
            }
            else
            {
                mostCommonBit = 0;
                leastCommonBit = 1;
            }

            return new List<int> { mostCommonBit, leastCommonBit };
        }

    }
}

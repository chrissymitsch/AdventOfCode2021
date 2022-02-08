using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode2021
{
    class Day01
    {
        public static void StartDay01()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day01\input.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                string line;
                List<int> lines = new List<int>();
                while (!String.IsNullOrEmpty(line = ReaderObject.ReadLine()))
                {
                    if (Int32.TryParse(line, out int lineAsNumber))
                    {
                        lines.Add(lineAsNumber);
                    }
                }
                Console.WriteLine("Part 1: " + countIncreased(lines));
                Console.WriteLine("Part 2: " + countIncreased(getSlidingWindows(lines)));
            }
        }

        private static List<int> getSlidingWindows(List<int> lines)
        {
            List<int> slidingWindows = new List<int>();
            for (var i = 0; i < lines.Count - 2; i++)
            {
                int line = lines[i];
                int slidingWindow = line;
                slidingWindow += lines[i + 1];
                slidingWindow += lines[i + 2];
                slidingWindows.Add(slidingWindow);
            }
            return slidingWindows;
        }

        private static int countIncreased(List<int> lines)
        {
            int before = 0;
            int count = 0;
            foreach (var line in lines)
            {
                if (before != 0 && line > before)
                {
                    count++;
                }
                before = line;
            }
            return count;
        }
    }
}

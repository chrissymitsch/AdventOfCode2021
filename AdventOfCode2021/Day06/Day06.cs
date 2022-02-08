using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    class Day06
    {
        public static void StartDay06()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day06\input.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                int[] startingLanternfish = prepareLanternfish(ReaderObject);
                Console.WriteLine("Part 1: " + waitForDays(startingLanternfish, 80));
                Console.WriteLine("Part 2: " + waitForDays(startingLanternfish, 256));
            }
        }

        private static int[] prepareLanternfish(StreamReader ReaderObject)
        {
            string line;
            int[] lanternfish = new int[0];
            while ((line = ReaderObject.ReadLine()) != null)
            {
                lanternfish = (line.Split(",")).Select(fish => Int32.Parse(fish)).ToArray();
            }
            return lanternfish;
        }

        private static long waitForDays(int[] originalLanternfish, int days)
        {
            var newFish = new long[9];
            foreach(var originalFish in originalLanternfish)
            {
                newFish[originalFish]++;
            }

            for (var i = 0; i < days; i++)
            {
                newFish[(i + 7) % 9] += newFish[i % 9];
            }
            return newFish.Sum();
        }
    }
}

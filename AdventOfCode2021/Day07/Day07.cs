using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    class Day07
    {
        public static void StartDay07()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day07\input.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                int[] startingPositions = preparePositions(ReaderObject);
                Console.WriteLine("Part 1: " + countFuel(startingPositions));
                Console.WriteLine("Part 2: " + countExpensiveFuel(startingPositions));
            }
        }

        private static int[] preparePositions(StreamReader ReaderObject)
        {
            string line;
            int[] positions = new int[0];
            while ((line = ReaderObject.ReadLine()) != null)
            {
                positions = (line.Split(",")).Select(position => Int32.Parse(position)).ToArray();
            }
            return positions;
        }

        private static long countFuel(int[] startingPositions)
        {
            long max = startingPositions.Max();
            long[] fuels = new long[max];
            for (var i = 0; i < max; i++)
            {
                fuels[i] = startingPositions.Select(pos => pos = Math.Abs(i - pos)).Sum();
            }
            return fuels.Min();
        }

        private static long countExpensiveFuel(int[] startingPositions)
        {
            long max = startingPositions.Max();
            long[] fuels = new long[max];
            for (var i = 0; i < max; i++)
            {
                fuels[i] = startingPositions.Select(pos => {
                    pos = Math.Abs(i - pos);
                    var result = 0;
                    while (pos > 0)
                    {
                        result += pos;
                        pos--;
                    }
                    return result;
                }).Sum();
            }
            return fuels.Min();
        }
    }
}

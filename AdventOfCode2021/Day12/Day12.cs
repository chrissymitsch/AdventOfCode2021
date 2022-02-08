using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode2021
{
    public class PathConnection
    {
        public string? bigA { get; set; }
        public string? bigB { get; set; }
        public string? smallA { get; set; }
        public string? smallB { get; set; }
    }
    class Day12
    {
        public static void StartDay12()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day12\test.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                List<PathConnection> inputLines = prepareInput(ReaderObject);
                //Console.WriteLine("Part 1: " + pathfinding(inputLines));
            }
        }

        private static List<PathConnection> prepareInput(StreamReader ReaderObject)
        {
            string line;
            List<PathConnection> input = new List<PathConnection>();
            while ((line = ReaderObject.ReadLine()) != null)
            {
                string[] splittedLine = line.Split("-");
                PathConnection con = new PathConnection();
                if (isBigCave(splittedLine[0]))
                {
                    con.bigA = splittedLine[0];
                }
                else
                {
                    con.smallA = splittedLine[0];
                }
                if (isBigCave(splittedLine[1]))
                {
                    con.bigB = splittedLine[1];
                }
                else
                {
                    con.smallB = splittedLine[1];
                }
                input.Add(con);
            }
            return input;
        }

        private static void pathfinding(List<PathConnection> cons)
        {
            List<PathConnection> startingPoints = cons.Where(con => con.smallA == "start").ToList();
            List<PathConnection> otherCons = cons.Where(con => con.smallA != "start").ToList();
            foreach (var start in startingPoints)
            {
                findPathToEnd(start, otherCons);
            }
        }

        private static void findPathToEnd(PathConnection start, List<PathConnection> otherCons)
        {
            List<PathConnection> nextPoints = otherCons.Where(con => con.smallA == start.smallA).ToList();
            nextPoints.AddRange(otherCons.Where(con => con.bigA == start.bigA).ToList());
            foreach (var next in nextPoints)
            {
                findPathToEnd(next, otherCons);
            }
        }

        private static bool isBigCave(string cave)
        {
            foreach (char c in cave)
            {
                if (!Char.IsUpper(c))
                    return false;
            }
            return true;
        }
    }
}

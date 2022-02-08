using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;

namespace AdventOfCode2021
{
    public class Point
    {
        public int index { get; set; }
        public int lineIndex { get; set; }
        public int value { get; set; }
    }

    class Day09
    {
        public static int basin = 0;
        public static List<string> checkedNeighbours = new List<string>();
        public static List<int[]> checklist = new List<int[]>();
        public static void StartDay09()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day09\input.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                List<string> inputLines = prepareInput(ReaderObject);
                List<Point> lowestPoints = getLowestPoints(inputLines);
                Console.WriteLine("Part 1: " + (lowestPoints.Sum(point => point.value) + lowestPoints.Count));
                Console.WriteLine("Part 2: " + getBasins(lowestPoints, inputLines));
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

        private static List<Point> getLowestPoints(List<string> inputLines)
        {
            List<Point> lowestPoints = new List<Point>();
            int lineIndex = 0;
            foreach(var line in inputLines)
            {
                for (var i = 0; i < line.Length; i++)
                {
                    int number = Int32.Parse(line[i].ToString());
                    if (number < 9 && isLowest(number, inputLines, i, lineIndex))
                    {
                        lowestPoints.Add(new Point
                        {
                            index = i,
                            lineIndex = lineIndex,
                            value = number
                        });
                    }
                }
                lineIndex++;
            }
            return lowestPoints;
        }

        private static bool isLowest(int number, List<string> inputLines, int index, int lineIndex)
        {
            var line = inputLines[lineIndex];
            var lineBefore = lineIndex > 0 ? inputLines[lineIndex - 1] : null;
            var lineAfter = lineIndex < inputLines.Count - 1 ? inputLines[lineIndex + 1] : null;
            bool hasLeftNeighbour = index > 0;
            bool hasRightNeighbour = index < line.Length - 1;
            if ((lineBefore != null && Int32.Parse(lineBefore[index].ToString()) <= number) ||
                (lineAfter != null && Int32.Parse(lineAfter[index].ToString()) <= number) ||
                (hasLeftNeighbour && Int32.Parse(line[index - 1].ToString()) <= number) ||
                (hasRightNeighbour && Int32.Parse(line[index + 1].ToString()) <= number))
            {
                return false;
            }
            return true;
        }

        private static int getBasins(List<Point> lowestPoints, List<string> inputLines)
        {
            // x = index, y = lineIndex
            List<int> results = new List<int>();
            foreach (var lowestPoint in lowestPoints)
            {
                var xy = new int[] { lowestPoint.index, lowestPoint.lineIndex };
                checklist.Add(xy);
                checkedNeighbours.Add(xy[0]+""+xy[1]);
                for (var i = lowestPoint.value; i < 10; i++)
                {
                    int breaking = 0;
                    int numberOfNeighbours = 0;
                    foreach (var check in checklist)
                    {
                        List<int[]> newChecklist = new List<int[]>();
                        newChecklist.AddRange(checklist);
                        var findNeigh = findNeighbours(check, i, inputLines);
                        if (findNeigh.Count > 0)
                        {
                            newChecklist.AddRange(findNeigh);
                            numberOfNeighbours = findNeigh.Count;
                            checklist = newChecklist;
                            List<int[]> copy = new List<int[]>(checklist);
                            int newRes = checkNeighbours(copy, inputLines, i);
                        }
                    }
                }
                checklist = new List<int[]>();
                results.Add(checkedNeighbours.Count);
                checkedNeighbours = new List<string>();
            }
            results.Sort();
            return results[results.Count - 1] * results[results.Count - 2] * results[results.Count - 3];
        }

        private static List<int[]> findNeighbours(int[] startingPoint, int num, List<string> inputLines)
        {
            List<int[]> neighbours = new List<int[]>();
            var n = new int[] { startingPoint[0] + 1, startingPoint[1] };
            if (!checkedNeighbours.Contains(n[0]+""+n[1]) && isValid(inputLines, n, num))
            {
                checkedNeighbours.Add(n[0] + "" + n[1]);
                neighbours.Add(n);
            }
            n = new int[] { startingPoint[0] - 1, startingPoint[1] };
            if (!checkedNeighbours.Contains(n[0] + "" + n[1]) && isValid(inputLines, n, num))
            {
                checkedNeighbours.Add(n[0] + "" + n[1]);
                neighbours.Add(n);
            }
            n = new int[] { startingPoint[0], startingPoint[1] + 1 };
            if (!checkedNeighbours.Contains(n[0] + "" + n[1]) && isValid(inputLines, n, num))
            {
                checkedNeighbours.Add(n[0] + "" + n[1]);
                neighbours.Add(n);
            }
            n = new int[] { startingPoint[0], startingPoint[1] - 1 };
            if (!checkedNeighbours.Contains(n[0] + "" + n[1]) && isValid(inputLines, n, num))
            {
                checkedNeighbours.Add(n[0] + "" + n[1]);
                neighbours.Add(n);
            }
            return neighbours;
        }
        
        private static int checkNeighbours(List<int[]> neighbours, List<string> inputLines, int num)
        {
            int result = 0;
            foreach (var neighbour in neighbours)
            {
                if (isValid(inputLines, neighbour, num))
                { 
                    result++;
                }
            }
            return result;
        }

        private static bool isValid(List<string> inputLines, int[] neighbour, int num)
        {
            bool valid = false;

            if (neighbour[0] >= 0 && neighbour[1] >= 0 && neighbour[0] < inputLines[0].Length && neighbour[1] < inputLines.Count)
            {
                int val = Int32.Parse(inputLines[neighbour[1]][neighbour[0]].ToString());
                if (val == num + 1 && val < 9)
                {
                    valid = true;
                }
            }

            return valid;
        }
    }
}

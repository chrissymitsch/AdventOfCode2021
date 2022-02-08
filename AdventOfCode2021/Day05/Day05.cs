using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    class Day05
    {
        public class Coordinates
        {
            public int x1 { get; set; }
            public int y1 { get; set; }
            public int x2 { get; set; }
            public int y2 { get; set; }
        }

        public class Danger
        {
            public int x { get; set; }
            public int[] y { get; set; }
        }

        public static void StartDay05()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day05\input.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                List<Coordinates> coordinatesList = prepareInput(ReaderObject);
                List<Danger> linesList_HV = drawHVLines(coordinatesList);
                int dangerPoints1 = findDangerousPoints(linesList_HV);
                Console.WriteLine("Part 1: " + dangerPoints1);

                List<Danger> linesList_HVD = drawHVDLines(coordinatesList);
                int dangerPoints2 = findDangerousPoints(linesList_HVD);
                Console.WriteLine("Part 2: " + dangerPoints2);
            }
        }

        private static List<Coordinates> prepareInput(StreamReader ReaderObject)
        {
            List<Coordinates> coordinatesList = new List<Coordinates>();
            string line;
            while (!String.IsNullOrEmpty(line = ReaderObject.ReadLine()))
            {
                Coordinates coordinates = new Coordinates();
                string[] splittedLine = line.Split(" -> ");
                string[] leftCoordinates = splittedLine[0].Split(",");
                string[] rightCoordinates = splittedLine[1].Split(",");
                coordinates.x1 = Int32.Parse(leftCoordinates[0]);
                coordinates.y1 = Int32.Parse(leftCoordinates[1]);
                coordinates.x2 = Int32.Parse(rightCoordinates[0]);
                coordinates.y2 = Int32.Parse(rightCoordinates[1]);
                coordinatesList.Add(coordinates);
            }
            return coordinatesList;
        }

        private static List<Danger> drawHVLines(List<Coordinates> coordinatesList)
        {
            List<Danger> dangerPoints = new List<Danger>();
            foreach(var coordinate in coordinatesList)
            {
                if (coordinate.x1 == coordinate.x2)
                {
                    Danger danger = new Danger();
                    danger.x = coordinate.x1;
                    if (coordinate.y1 < coordinate.y2)
                    {
                        danger.y = Enumerable.Range(coordinate.y1, (coordinate.y2 - coordinate.y1) + 1).ToArray();
                    }
                    else
                    {
                        danger.y = Enumerable.Range(coordinate.y2, (coordinate.y1 - coordinate.y2) + 1).ToArray();
                    }
                    dangerPoints.Add(danger);
                }
                if (coordinate.y1 == coordinate.y2)
                {
                    List<int> allX = new List<int>();
                    if (coordinate.x1 < coordinate.x2)
                    {
                        allX = Enumerable.Range(coordinate.x1, (coordinate.x2 - coordinate.x1) + 1).ToList();
                    }
                    else
                    {
                        allX = Enumerable.Range(coordinate.x2, (coordinate.x1 - coordinate.x2) + 1).ToList();
                    }

                    foreach (var x in allX)
                    {
                        Danger danger = new Danger();
                        danger.y = new int[1];
                        danger.y[0] = coordinate.y1;
                        danger.x = x;
                        dangerPoints.Add(danger);
                    }
                }
            }
            return dangerPoints;
        }

        private static List<Danger> drawHVDLines(List<Coordinates> coordinatesList)
        {
            List<Danger> dangerPoints = drawHVLines(coordinatesList);
            foreach (var coordinate in coordinatesList)
            {
                if (Math.Abs(coordinate.x1 - coordinate.x2) == Math.Abs(coordinate.y1 - coordinate.y2))
                {
                    List<int> allX;
                    List<int> allY;
                    if (coordinate.x1 < coordinate.x2)
                    {
                        allX = Enumerable.Range(coordinate.x1, (coordinate.x2 - coordinate.x1) + 1).ToList();
                        if (coordinate.y1 < coordinate.y2)
                        {
                            allY = Enumerable.Range(coordinate.y1, (coordinate.y2 - coordinate.y1) + 1).ToList();
                        }
                        else
                        {
                            allY = Enumerable.Range(coordinate.y2, (coordinate.y1 - coordinate.y2) + 1).ToList();
                            allY.Reverse();
                        }
                    }
                    else
                    {
                        allX = Enumerable.Range(coordinate.x2, (coordinate.x1 - coordinate.x2) + 1).ToList();
                        if (coordinate.y1 < coordinate.y2)
                        {
                            allY = Enumerable.Range(coordinate.y1, (coordinate.y2 - coordinate.y1) + 1).ToList();
                            allY.Reverse();
                        }
                        else
                        {
                            allY = Enumerable.Range(coordinate.y2, (coordinate.y1 - coordinate.y2) + 1).ToList();
                        }
                    }
                    for (var i = 0; i < allX.Count; i++)
                    {
                        Danger danger = new Danger();
                        danger.y = new int[1];
                        danger.y[0] = allY[i];
                        danger.x = allX[i];
                        dangerPoints.Add(danger);
                    }
                }
            }
            return dangerPoints;
        }

        private static int findDangerousPoints(List<Danger> linesList)
        {
            var groupedList = linesList.GroupBy(group => group.x).ToList();

            var count = 0;
            int[][] neu = new int[1000][];
            foreach(var group in groupedList)
            {
                List<int> yList = new List<int>();
                List<int[]> yMany = new List<int[]>();
                foreach(var x in group)
                {
                    yMany.Add(x.y);
                }
                yList = yMany.SelectMany(y => y).ToList();
                var test = yList.GroupBy(y => y).Where(g => g.Count() > 1).ToList();
                count += test.Select(y => y.Key).ToList().Count;
            }

            return count;
        }
    }
}

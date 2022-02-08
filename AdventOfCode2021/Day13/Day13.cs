using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2021
{
    public class XY
    {
        public int? x { get; set; }
        public int? y { get; set; }
    }

    class Day13
    {
        public static void StartDay13()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day13\input.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                List<XY> inputLines = prepareInput(ReaderObject);
                List<XY> instructions = new List<XY>();
                /*
fold along x=655
fold along y=447
fold along x=327
fold along y=223
fold along x=163
fold along y=111
fold along x=81
fold along y=55
fold along x=40
fold along y=27
fold along y=13
fold along y=6
                */
                instructions.Add(new XY { x = 655 });
                instructions.Add(new XY { y = 447 });
                instructions.Add(new XY { x = 327 });
                instructions.Add(new XY { y = 223 });
                instructions.Add(new XY { x = 163 });
                instructions.Add(new XY { y = 111 });
                instructions.Add(new XY { x = 81 });
                instructions.Add(new XY { y = 55 });
                instructions.Add(new XY { x = 40 });
                instructions.Add(new XY { y = 27 });
                instructions.Add(new XY { y = 13 });
                instructions.Add(new XY { y = 6 });
                string[] sheet = drawSheet(inputLines);
                Console.WriteLine("Part 1: " + fold(sheet, instructions));
            }
        }

        private static List<XY> prepareInput(StreamReader ReaderObject)
        {
            string line;
            List<XY> input = new List<XY>();
            while ((line = ReaderObject.ReadLine()) != null)
            {
                string[] splittedLine = line.Split(",");
                input.Add(new XY() { x = Int32.Parse(splittedLine[0]), y = Int32.Parse(splittedLine[1]) });
            }
            return input;
        }

        private static string[] drawSheet(List<XY> input)
        {
            int width = (int)input.Max(p => p.x);
            int height = (int)input.Max(p => p.y);
            string[] sheet = new string[height + 1];
            for (var j = 0; j < height + 1; j++)
            {
                string line = "";
                for (var i = 0; i < width + 1; i++)
                {
                    line += ".";
                }
                sheet[j] = line;
            }
            foreach (var point in input)
            {
                var line = sheet[(int)point.y];
                StringBuilder sb = new StringBuilder(line);
                sb[(int)point.x] = Convert.ToChar("#");
                line = sb.ToString();
                sheet[(int)point.y] = line;
            }
            return sheet;
        }

        private static int fold(string[] sheet, List<XY> instructions)
        {
            int result = 0;
            foreach (var instruction in instructions)
            {
                var foldLine = -1;
                bool transed = false;
                if (instruction.x != null)
                {
                    // fold left
                    sheet = transposeSheet(sheet);
                    transed = true;
                    foldLine = (int)instruction.x;
                }
                else if (instruction.y != null)
                {
                    foldLine = (int)instruction.y;
                }

                if (foldLine > -1)
                {
                    // fold up
                    string[] newSheet = sheet.Take(foldLine).ToArray();
                    string[] foldSheet = sheet.Skip(foldLine + 1).ToArray();
                    for (var i = 0; i < foldSheet.Length; i++)
                    {
                        var line = newSheet[newSheet.Length - 1 - i];
                        for (var j = 0; j < line.Length; j++)
                        {
                            if (foldSheet[i][j] == Convert.ToChar("#"))
                            {
                                StringBuilder sb = new StringBuilder(line);
                                sb[(int)j] = Convert.ToChar("#");
                                line = sb.ToString();
                            }
                        }
                        newSheet[newSheet.Length - 1 - i] = line;
                    }
                    sheet = newSheet;
                    if (transed)
                    {
                        sheet = transposeSheet(sheet);
                    }
                }
            }
            foreach(var line in sheet)
            {
                Console.WriteLine(line);
            }
            return sheet.Sum(line => line.Count(c => c.ToString() == "#"));
        }

        public static string[] transposeSheet(string[] sheet)
        {
            int m = sheet.Length;
            int n = sheet[0].Length;

            string[] transposedSheet = new string[n];

            for (int x = 0; x < n; x++)
            {
                char[] line = new char[m];
                for (int y = 0; y < m; y++)
                {
                    line[y] = sheet[y][x];
                }
                transposedSheet[x] = new string(line);
            }

            return transposedSheet;
        }
    }
}

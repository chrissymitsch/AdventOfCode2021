using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2021
{
    class Day04
    {
        public static int boardSize = 5;
        public static int boardLength = 14;
        public static List<int> winningBoards = new List<int>();

        public static void StartDay04()
        {
            List<List<string[]>> bingoBoards = new List<List<string[]>>();
            string FileToRead_Input = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day04\input.txt";
            string FileToRead_Boards = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day04\input-boards.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead_Boards))
            {
                bingoBoards = prepareBoards(ReaderObject);
            }
            using (StreamReader ReaderObject = new StreamReader(FileToRead_Input))
            {
                List<string> bingoInputs = prepareInput(ReaderObject);
                List<string[]> lastWinningBingoBoard = new List<string[]>();
                foreach (var bingoInput in bingoInputs)
                {
                    //search in all boards and mark with x
                    bingoBoards = crossInputInBoards(bingoInput, bingoBoards);
                    //if (findBingoInBoards(bingoInput, bingoBoards)) PART 1
                    //{
                    //    break;
                    //}
                    findBingoInBoards(bingoInput, bingoBoards);
                    if (winningBoards.Count == bingoBoards.Count)
                    {
                        Console.WriteLine(calculateUnmarkedNumbers(bingoBoards[winningBoards.Last()]) * Int32.Parse(bingoInput));
                        break;
                    }
                }
            }
        }

        private static List<List<string[]>> prepareBoards(StreamReader ReaderObject)
        {
            string line;
            List<List<string[]>> boards = new List<List<string[]>>();
            List<string[]> board = new List<string[]>();
            while ((line = ReaderObject.ReadLine()) != null && line.Length >= 0)
            {
                if (line.Length == boardLength)
                {
                    line = line.Trim();
                    line = Regex.Replace(line, @"\s+", " ");
                    string[] splittedLine = line.Split(" ");
                    board.Add(splittedLine);
                    if (board.Count == boardSize)
                    {
                        boards.Add(board);
                        board = new List<string[]>();
                    }
                }
            }
            return boards;
        }

        private static List<string> prepareInput(StreamReader ReaderObject)
        {
            string inputLine;
            List<string> inputs = new List<string>();
            while (!String.IsNullOrEmpty(inputLine = ReaderObject.ReadLine()))
            {
                inputs = inputLine.Split(",").ToList();
            }
            return inputs;
        }

        private static List<List<string[]>> crossInputInBoards(string input, List<List<string[]>> bingoBoards)
        {
            List<List<string[]>> newBingoBoards = new List<List<string[]>>();
            foreach (var bingoBoard in bingoBoards)
            {
                List<string[]> newBoard = new List<string[]>();
                foreach (var bingoLine in bingoBoard)
                {
                    string[] newLine = bingoLine.Select(val => val == input ? "x" : val).ToArray();
                    newBoard.Add(newLine);
                }
                newBingoBoards.Add(newBoard);
            }
            return newBingoBoards;
        }

        //private static bool findBingoInBoards(string bingoInput, List<List<string[]>> bingoBoards) PART 1
        private static void findBingoInBoards(string bingoInput, List<List<string[]>> bingoBoards)
        {
            List<string[]> lastWinningBoard = new List<string[]>();
            //foreach (var bingoBoard in bingoBoards) PART 1
            for (var boardIndex = 0; boardIndex < bingoBoards.Count; boardIndex++)
            {
                List<string[]> bingoBoard = bingoBoards[boardIndex];
                // Horizontal
                foreach (var bingoLine in bingoBoard)
                {
                    if (bingoLine.Count(val => val == "x") == boardSize)
                    {
                        //Console.WriteLine("BINGO H");
                        //Console.WriteLine(calculateUnmarkedNumbers(bingoBoard) * Int32.Parse(bingoInput));
                        //return true; PART 1
                        itsABingo(boardIndex);
                    }
                }

                // Vertical
                for (var i = 0; i < boardSize; i++)
                {
                    if (bingoBoard.Where(line => line[i] == "x").ToList().Count == boardSize)
                    {
                        //Console.WriteLine("BINGO V");
                        //Console.WriteLine(calculateUnmarkedNumbers(bingoBoard) * Int32.Parse(bingoInput));
                        //return true; PART 1
                        itsABingo(boardIndex);
                    }
                }
            }
        }

        private static int calculateUnmarkedNumbers(List<string[]> bingoBoard)
        {
            return bingoBoard.Sum(line => line.Sum(val => val != "x" ? Int32.Parse(val) : 0));
        }

        private static void itsABingo(int boardIndex)
        {
            if (!winningBoards.Contains(boardIndex))
            {
                winningBoards.Add(boardIndex);
            }
        }
    }
}

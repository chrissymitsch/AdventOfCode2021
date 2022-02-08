using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode2021
{
    enum DIRECTIONS
    {
        forward, up, down
    }

    public class Instruction
    {
        public string Direction { get; set; }
        public int Value { get; set; }
    }

    class Day02
    {
        public static void StartDay02()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day02\input.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                string line;
                List<Instruction> lines = new List<Instruction>();
                while (!String.IsNullOrEmpty(line = ReaderObject.ReadLine()))
                {
                    string[] instructionValue = line.Split(" ");
                    Int32.TryParse(instructionValue[1], out int parsedValue);
                    Instruction instruction = new Instruction
                    {
                        Direction = instructionValue[0],
                        Value = parsedValue
                    };
                    lines.Add(instruction);
                }
                Console.WriteLine("Part 1: " + getPosition(lines));
                Console.WriteLine("Part 2: " + getAim(lines));
            }
        }

        private static int getAim(List<Instruction> lines)
        {
            int horizontal = 0;
            int depth = 0;
            int aim = 0;

            foreach (var instruction in lines)
            {
                if (instruction.Direction == Enum.GetName(DIRECTIONS.forward))
                {
                    horizontal += instruction.Value;
                    depth += instruction.Value * aim;
                }
                else if (instruction.Direction == Enum.GetName(DIRECTIONS.down))
                {
                    aim += instruction.Value;
                }
                else if (instruction.Direction == Enum.GetName(DIRECTIONS.up))
                {
                    aim -= instruction.Value;
                }
            }
            return horizontal * depth;
        }

        private static int getPosition(List<Instruction> lines)
        {
            int horizontal = 0;
            int depth = 0;

            foreach (var instruction in lines)
            {
                if (instruction.Direction == Enum.GetName(DIRECTIONS.forward))
                {
                    horizontal += instruction.Value;
                }
                else if (instruction.Direction == Enum.GetName(DIRECTIONS.down))
                {
                    depth += instruction.Value;
                }
                else if (instruction.Direction == Enum.GetName(DIRECTIONS.up))
                {
                    depth -= instruction.Value;
                }
            }
            return horizontal * depth;
        }
    }
}

using System;

namespace AdventOfCode2021
{
    class AdventOfCode
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Which day is today? (1-25)");
                string day = Console.ReadLine();
                switch (day)
                {
                    case "1":
                        Day01.StartDay01();
                        break;
                    case "2":
                        Day02.StartDay02();
                        break;
                    case "3":
                        Day03.StartDay03();
                        break;
                    case "4":
                        Day04.StartDay04();
                        break;
                    case "5":
                        Day05.StartDay05();
                        break;
                    case "6":
                        Day06.StartDay06();
                        break;
                    case "7":
                        Day07.StartDay07();
                        break;
                    case "8":
                        Day08.StartDay08();
                        break;
                    case "9":
                        Day09.StartDay09();
                        break;
                    case "10":
                        Day10.StartDay10();
                        break;
                    case "11":
                        Day11.StartDay11();
                        break;
                    case "13":
                        Day13.StartDay13();
                        break;
                    case "14":
                        Day14.StartDay14();
                        break;
                    case "15":
                        Day15.StartDay15();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

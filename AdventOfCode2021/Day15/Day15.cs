using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode2021
{
    class Day15
    {
        class Node
        {
            public int x { get; set; }
            public int y { get; set; }
            public int risk { get; set; }
            public int distance { get; set; } = int.MaxValue;
            public bool visited { get; set; } = false;

            public Node(int x, int y, int risk)
            {
                this.x = x;
                this.y = y;
                this.risk = risk;
            }
        }

        public static (int, int)[] adjacent = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

        public static void StartDay15()
        {
            string FileToRead = @"C:\Users\christine.mitsch\source\repos\AdventOfCode2021\AdventOfCode2021\Day15\input.txt";
            using (StreamReader ReaderObject = new StreamReader(FileToRead))
            {
                var cave = prepareInput(ReaderObject);
                var width = (int) Math.Sqrt(cave.Count);
                Console.WriteLine("Part 1: " + Dijkstra(cave, cave[(width - 1, width - 1)]));

                var quintupleCave = prepareBiggerCave(width, cave);
                width *= 5;
                Console.WriteLine("Part 2: " + Dijkstra(quintupleCave, quintupleCave[(width - 1, width - 1)]));
            }
        }

        private static Dictionary<(int, int), Node> prepareInput(StreamReader ReaderObject)
        {
            string line;
            List<int[]> lines = new List<int[]>();
            while ((line = ReaderObject.ReadLine()) != null)
            {
                int[] riskLevels = new int[line.Length];
                for (var i = 0; i < line.Length; i++)
                {
                    riskLevels[i] = Int32.Parse(line[i].ToString());
                }
                lines.Add(riskLevels);
            }
            return lines
                .SelectMany((line, y) => line.Select((c, x) => ((x, y), new Node(x, y, Int32.Parse(c.ToString())))))
                .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
        }

        private static Dictionary<(int, int), Node> prepareBiggerCave(int width, Dictionary<(int, int), Node> cave)
        {
            return Enumerable.Range(0, 5).SelectMany(i => Enumerable.Range(0, 5).SelectMany(j =>
                cave.Select(kvp => {
                    (int x, int y) newKey = (kvp.Key.Item1 + width * i, kvp.Key.Item2 + width * j);
                    var newRisk = (kvp.Value.risk + i + j - 1) % 9 + 1;
                    return (newKey, new Node(newKey.x, newKey.y, newRisk));
                })))
                .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
        }

        private static int Dijkstra(Dictionary<(int, int), Node> graph, Node end)
        {
            var next = new PriorityQueue<Node, int>();
            graph[(0, 0)].distance = 0;
            next.Enqueue(graph[(0, 0)], 0);
            while (next.Count > 0)
            {
                var current = next.Dequeue();
                if (current.visited)
                {
                    continue;
                }

                current.visited = true;

                if (current == end)
                {
                    return end.distance;
                }

                foreach (var neighbor in getNeighbors(graph, current))
                {
                    var alt = current.distance + neighbor.risk;
                    if (alt < neighbor.distance)
                    {
                        neighbor.distance = alt;
                    }

                    if (neighbor.distance != int.MaxValue)
                    {
                        next.Enqueue(neighbor, neighbor.distance);
                    }
                }
            }
            return end.distance;
        }

        private static List<Node> getNeighbors(Dictionary<(int, int), Node> graph, Node node)
        {
            List<Node> neighbours = new List<Node>();
            foreach ((int i, int j) in adjacent)
            {
                var key = (node.x + i, node.y + j);
                if (graph.ContainsKey(key) && !graph[key].visited)
                {
                    neighbours.Add(graph[key]);
                }
            }
            return neighbours;
        }
    }
}

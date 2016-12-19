using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    struct Node
    {
        public Tuple<int, int> Coord;
        public int dist;
        public string currPath;

        public Node(Tuple<int, int> c, int d, string p)
        {
            Coord = c;
            dist = d;
            currPath = p;
        }
    }

    class Program
    {
        static int layoutWidth = 4;
        static int layoutHeight = 4;

        static string CalculateHash(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

        static bool isDoorOpen(char c)
        {
            char[] open = new char[] { 'b', 'c', 'd', 'e', 'f' };
            if (open.Contains(c))
            {
                return true;
            }
            return false;
        }

        static bool isValid(int x, int y)
        {
            return (x >= 0) && (x < layoutWidth) && (y >= 0) && (y < layoutHeight);
        }

        static string BFS(Tuple<int, int> start, Tuple<int, int> end, string input, bool longest)
        {
            Queue<Node> queue = new Queue<Node>();
            Node root = new Node(start, 0, input);
            queue.Enqueue(root);
            string path = input;
            string output = "";
            int[] rowNum = { 0, 0, -1, 1 };
            int[] colNum = { -1, 1, 0, 0 };
            string[] dir = { "U", "D", "L", "R" };

            while (queue.Count > 0)
            {
                Node curr = queue.Dequeue();
                Tuple<int, int> Loc = curr.Coord;

                if (Loc.Item1 == end.Item1 && Loc.Item2 == end.Item2)
                {
                    if (longest)
                    {
                        output = curr.currPath;
                        continue;
                    }
                    return curr.currPath;           
                }

                path = curr.currPath;
                string hash = CalculateHash(path).Substring(0, 4);

                for (int i = 0; i < 4; i++)
                {
                    if (isDoorOpen(hash[i]))
                    {
                        int nRow = Loc.Item1 + rowNum[i];
                        int nCol = Loc.Item2 + colNum[i];

                        if (isValid(nRow, nCol))
                        {
                            Node adj = new Node(Tuple.Create(nRow, nCol), curr.dist + 1, path + dir[i]);
                            queue.Enqueue(adj);
                        }
                    }
                }
            }

            return output;
        }

        static void Main(string[] args)
        {
            string input = "veumntbg";
            Tuple<int, int> start = Tuple.Create(0, 0);
            Tuple<int, int> end = Tuple.Create(3, 3);

            string shortestPath = BFS(start, end, input, false).Substring(input.Length);
            int longestPath = BFS(start, end, input, true).Substring(input.Length).Length;

            Console.WriteLine("The Shortest path to reach the vault is: " + shortestPath);
            Console.WriteLine("The Longest path length to reach the value is: " + longestPath);
            Console.Read();
        }
    }
}

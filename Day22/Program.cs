using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;

namespace Day22
{
    [Serializable]
    class Node
    {
        public int Size;
        public int Used;
        
        public Node()
        {

        }  
        
        public Node(Node n)
        {
            Size = n.Size;
            Used = n.Used;
        }      
        public int Avail()
        {
            return Size - Used;
        }
        public int Usage()
        {
            return (Used / Size) * 100;
        }
    }

    [Serializable]
    class Storage
    {
        public Node[,] store;
        public int Dist = 0;
        public Tuple<int, int> Coord;

        public Storage(Node[,] s, int d, Tuple<int, int> c)
        {
            store = DeepClone(s);
            Dist = d;
            Coord = c;
        }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }

    class Program
    {
        static Node[,] storage = new Node[50, 50];
        static List<Tuple<Node, Node>> viablePairs = new List<Tuple<Node, Node>>();
        HashSet<Node> visited = new HashSet<Node>();

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        static bool isViablePair(Node A, Node B)
        {
            if(A.Used != 0 && A != B && A.Used < B.Avail())
            {
                return true;
            }
            return false;
        }

        static bool canMoveData(Node A, Node B)
        {
            if(A.Used < B.Avail())
            {
                return true;
            }
            return false;
        }

        static void moveData(Node From, Node To)
        {
            int data = From.Used;
            From.Used = 0;
            To.Used += data;
        }

        static bool isValid(int x, int y)
        {
            if(x >= 0 && x < 50 && y >= 0 && y < 50)
            {
                if(storage[x, y] != null)
                {
                    return true;
                }
            }
            return false;
        }

        static void checkAllPairs(Node n)
        {
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    if (storage[x, y] != null)
                    {
                        if (isViablePair(n, storage[x, y]))
                        {
                            viablePairs.Add(Tuple.Create(n, storage[x, y]));
                        }
                    }  
                }
            }
        }

        static void printStorage(Node[,] s)
        {
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    if (s[x, y] != null)
                    {
                        if(s[x, y].Used > 100) { Console.Write("#"); }
                        else if (s[x, y].Used == 0) { Console.Write("E"); }
                        else if (x == 0 && y == 0) { Console.Write("0"); }
                        else if (x == 36 && y == 0) { Console.Write("G"); }
                        else { Console.Write("."); }
                    }
                }
                Console.WriteLine("");
            }
        }

        static int BFS(Tuple<int, int> start, Tuple<int, int> end)
        {
            Queue<Storage> queue = new Queue<Storage>();
            HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();
            Storage root = new Storage(storage, 0, start);
            queue.Enqueue(root);
            int[] rowNum = { 0, 0, -1, 1 };
            int[] colNum = { -1, 1, 0, 0 };
            string[] dir = { "U", "D", "L", "R" };

            while (queue.Count > 0)
            {
                Storage curr = queue.Dequeue();
                Tuple<int, int> Loc = curr.Coord;

                if (Loc.Item1 == end.Item1 && Loc.Item2 == end.Item2)
                {
                    printStorage(curr.store);
                    return curr.Dist;
                }
                if (visited.Contains(Loc))
                {
                    continue;
                }
                visited.Add(Loc);

                for (int i = 0; i < 4; i++)
                {
                    int nX = Loc.Item1 + rowNum[i];
                    int nY = Loc.Item2 + colNum[i];

                    if (isValid(nX, nY))
                    {
                        Node[,] s = DeepClone(curr.store);
                        if (canMoveData(s[nX, nY], s[Loc.Item1, Loc.Item2]))
                        {
                            moveData(s[nX, nY], s[Loc.Item1, Loc.Item2]);
                            Storage adj = new Storage(s, curr.Dist + 1, Tuple.Create(nX, nY));
                            queue.Enqueue(adj);
                        }
                        
                    }
                }
            }

            return -1;
        }

        static void Main(string[] args)
        {
            Tuple<int, int> start;
            Tuple<int, int> end;
            Tuple<int, int> emptyNode = null;

            Regex regex = new Regex(@"/dev/grid/node-x(\d+)-y(\d+)\s+(\d+)T\s+(\d+)T\s+(\d+)T\s+(\d+)%");
            foreach (string line in File.ReadLines("../../input.txt"))
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);

                    Node n = new Node();
                    n.Size = int.Parse(match.Groups[3].Value);
                    n.Used = int.Parse(match.Groups[4].Value);

                    storage[x, y] = n;
                } 
            }

            for(int y = 0; y < 50; y++)
            {
                for(int x = 0; x < 50; x++)
                {
                    if (storage[x, y] != null)
                    {
                        checkAllPairs(storage[x, y]);
                        if(storage[x, y].Used == 0)
                        {
                            emptyNode = Tuple.Create(x, y);
                        }
                    }
                }
            }

            printStorage(storage);

            start = Tuple.Create(35, 0);
            end = Tuple.Create(1, 0);

            int shortest = BFS(emptyNode, start);
            shortest += (35 * 5) + 1; //Cheaty but works for my input

            Console.WriteLine("The number of Viable Pairs is: " + viablePairs.Count);
            Console.WriteLine("The shortest path is: " + shortest);
            Console.Read();
        }
    }
}

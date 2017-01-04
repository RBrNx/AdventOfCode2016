using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{

    public class PermuteUtils
    {
        // Returns an enumeration of enumerators, one for each permutation
        // of the input.
        public static IEnumerable<IEnumerable<T>> Permute<T>(IEnumerable<T> list, int count)
        {
            if (count == 0)
            {
                yield return new T[0];
            }
            else
            {
                int startingElementIndex = 0;
                foreach (T startingElement in list)
                {
                    IEnumerable<T> remainingItems = AllExcept(list, startingElementIndex);

                    foreach (IEnumerable<T> permutationOfRemainder in Permute(remainingItems, count - 1))
                    {
                        yield return Concat<T>(
                            new T[] { startingElement },
                            permutationOfRemainder);
                    }
                    startingElementIndex += 1;
                }
            }
        }

        // Enumerates over contents of both lists.
        public static IEnumerable<T> Concat<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            foreach (T item in a) { yield return item; }
            foreach (T item in b) { yield return item; }
        }

        // Enumerates over all items in the input, skipping over the item
        // with the specified offset.
        public static IEnumerable<T> AllExcept<T>(IEnumerable<T> input, int indexToSkip)
        {
            int index = 0;
            foreach (T item in input)
            {
                if (index != indexToSkip) yield return item;
                index += 1;
            }
        }
    }

    class Program
    {
        static string[] input = File.ReadAllLines("../../input.txt");
        static char[][] grid = new char[input.Length][];
        static Tuple<int, int> start;
        static int[] distsFromZero = new int[8];
        static Tuple<int, int>[] POIs = new Tuple<int, int>[8];
        static HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();
        static List<int[]> permutations = new List<int[]>();

        static int[] rowNum = { -1, 0, 0, 1 };
        static int[] colNum = { 0, -1, 1, 0 };

        static int Part1 = int.MaxValue;
        static int Part2 = int.MaxValue;

        struct Node
        {
            public Tuple<int, int> Coord;
            public int dist;

            public Node(Tuple<int, int> c, int d)
            {
                Coord = c;
                dist = d;
            }
        }

        static int BFS(Tuple<int, int> start, Tuple<int, int> end)
        {
            Queue<Node> queue = new Queue<Node>();
            Node root = new Node(start, 0);
            queue.Enqueue(root);
            visited.Clear();

            while (queue.Count > 0)
            {
                Node curr = queue.Dequeue();
                Tuple<int, int> Loc = curr.Coord;

                if (Loc.Item1 == end.Item1 && Loc.Item2 == end.Item2)
                {
                    return curr.dist;
                }

                for (int i = 0; i < 4; i++)
                {
                    int nRow = Loc.Item1 + rowNum[i];
                    int nCol = Loc.Item2 + colNum[i];

                    if (isValid(nRow, nCol) && grid[nRow][nCol] != '#' && !visited.Contains(Tuple.Create(nRow, nCol)))
                    {
                        visited.Add(Tuple.Create(nRow, nCol));
                        Node adj = new Node(Tuple.Create(nRow, nCol), curr.dist + 1);
                        queue.Enqueue(adj);
                    }
                }
            }

            return -1;
        }

        static bool isValid(int y, int x)
        {
            return (x >= 0) && (x < grid[0].Length) && (y >= 0) && (y < grid.Length);
        }

        static void Main(string[] args)
        {

            for(int i = 0; i < input.Length; i++)
            {
                char[] row = input[i].ToCharArray();
                int pos = input[i].IndexOf('0');
                if (pos >= 0) start = Tuple.Create(i, pos);
                grid[i] = row;
            }

            for (int i = 0; i < grid.Length; i++)
            {
                for(int j = 0; j < grid[i].Length; j++)
                {
                    if (char.IsDigit(grid[i][j]))
                    {
                        int x = int.Parse(grid[i][j].ToString());
                        POIs[x] = Tuple.Create(i, j);
                        distsFromZero[x] = BFS(start, Tuple.Create(i, j));
                    }
                }
            }

            int[,] dists = new int[POIs.Length, POIs.Length];

            for(int i = 1; i < POIs.Length - 1; i++)
            {
                for(int j = i+1; j < POIs.Length; j++)
                {
                    dists[i, j] = dists[j, i] = BFS(POIs[i], POIs[j]);
                }               
            }

            foreach(IEnumerable<int> perm in PermuteUtils.Permute<int>(Enumerable.Range(1, POIs.Length - 1), POIs.Length - 1))
            {
                int[] arr = perm.ToArray();
                int dist = distsFromZero[arr[0]];
                for(int i = 0; i < arr.Length - 1; i++)
                {
                    dist += dists[arr[i], arr[i + 1]];
                }
                Part1 = Math.Min(Part1, dist);
                dist += distsFromZero[arr[arr.Length - 1]];
                Part2 = Math.Min(Part2, dist);
            }

            Console.WriteLine("The shortest number of steps is: " + Part1);
            Console.WriteLine("The shortest number of steps back to 0 is: " + Part2);
            Console.Read();
        }
    }
}

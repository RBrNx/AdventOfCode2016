using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
        static Tuple<int, int> start = Tuple.Create(1, 1);
        static Tuple<int, int> goal = Tuple.Create(31, 39);
        static int input = 1352;
        static char[,] maze = new char[500, 500];
        static bool[,] visited = new bool[500, 500];

        static int[] rowNum = { -1, 0, 0, 1 };
        static int[] colNum = { 0, -1, 1, 0 };

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

        static string getBinary(int x, int y)
        {
            int sum = (x * x) + (3 * x) + (2 * x * y) + y + (y * y);
            sum += input;
            return Convert.ToString(sum, 2);
        }

        static int shortestRoute()
        {
            return 0;
        }

        static void outputMaze()
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                for (int x = 0; x < maze.GetLength(0); x++)
                {
                    Console.Write(maze[x, y]);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("\n");
        }

        static bool isValid(int x, int y)
        {
            return (x >= 0) && (x < maze.GetLength(0)) && (y >= 0) && (y < maze.GetLength(1));
        }

        static int BFS(Tuple<int, int> start, Tuple<int, int> end)
        {
            visited[start.Item1, start.Item2] = true;
            Queue<Node> queue = new Queue<Node>();
            Node root = new Node(start, 0);
            queue.Enqueue(root);

            while(queue.Count > 0)
            {
                Node curr = queue.Dequeue();
                Tuple<int, int> Loc = curr.Coord;

                if(Loc.Item1 == end.Item1 && Loc.Item2 == end.Item2)
                {
                    return curr.dist;
                }

                if(curr.dist == 50) //Part 2 (Remove for Part 1 to Complete)
                {
                    return 0;
                }

                for(int i = 0; i < 4; i++)
                {
                    int nRow = Loc.Item1 + rowNum[i];
                    int nCol = Loc.Item2 + colNum[i];

                    if(isValid(nRow, nCol) && maze[nRow, nCol] != '#' && !visited[nRow, nCol])
                    {
                        visited[nRow, nCol] = true;
                        Node adj = new Node(Tuple.Create(nRow, nCol), curr.dist + 1);
                        queue.Enqueue(adj);
                    }
                }
            }

            return -1;
        }

        static void Main(string[] args)
        {
            for(int y = 0; y < maze.GetLength(1); y++)
            {
                for(int x = 0; x < maze.GetLength(0); x++)
                {
                    string binary = getBinary(x, y);
                    int oneCount = binary.Count(c => c == '1');
                    if(oneCount % 2 == 0)
                    {
                        maze[x, y] = ' ';
                    }
                    else
                    {
                        maze[x, y] = '#';
                    }

                    if(x == goal.Item1 && y == goal.Item2)
                    {
                        maze[x, y] = 'x';
                    }
                    if (x == start.Item1 && y == start.Item2)
                    {
                        maze[x, y] = '+';
                    }
                }
            }

            //outputMaze();

            int steps = BFS(start, goal);
            if(steps == 0)
            {
                int locations = 0;
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    for (int x = 0; x < maze.GetLength(0); x++)
                    {
                        if (visited[x, y]) locations++;
                    }
                }

                Console.WriteLine("The number of Distinct Location within 50 Steps is: " + locations);
            }
            
            Console.WriteLine("The shortest steps to " + goal.ToString() + " is: " + steps);
            Console.Read();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    class Node
    {
        public List<Tuple<int, int>> pairs;
        public int FloorNum;
        public int distance;

        public Node(List<Tuple<int, int>> p, int fN, int dist)
        {
            pairs = p;
            FloorNum = fN;
            distance = dist;
        }

        public Node()
        {

        }
    }

    class Program
    {    
        static List<Tuple<int, int>> pairs = new List<Tuple<int, int>>() { Tuple.Create(0, 1), Tuple.Create(0, 2) };
        static HashSet<Node> hash = new HashSet<Node>();

        static void Main(string[] args)
        {
            Node startNode = new Node(pairs, 0, 0);
            int moveCount = 0;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(startNode);
            hash.Add(startNode);

            while(queue.Count > 0)
            {
                //If Floor 4 contains all items - Solved!

                Node current = queue.Dequeue();
                //Get All (Valid) Successor Nodes
                //Add them to the queue
                //Add them to the hash
            }
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    class Program
    {
        static int time;
        static int prevTime = -1;
        static bool capsuleDropped = false;
        static int ballPos;
        static bool running;
        static List<Tuple<int, int, int>> DiscPositions = new List<Tuple<int, int, int>>();
        static List<List<Tuple<int, int, int>>> Cache = new List<List<Tuple<int, int, int>>>();

        static void calculatePositionsAtTime(int time)
        {
            if(time < Cache.Count)
            {
                DiscPositions = Cache[time].ToList();
            }
            else
            {
                for (int i = 0; i < DiscPositions.Count; i++)
                {
                    int startingPos = DiscPositions[i].Item2;
                    int numPos = DiscPositions[i].Item1;
                    int newPos = startingPos;
                    for (int j = 0; j < time; j++)
                    {
                        if (newPos + 1 < numPos)
                        {
                            newPos++;
                        }
                        else
                        {
                            newPos = 0;
                        }
                    }
                    DiscPositions[i] = Tuple.Create(numPos, startingPos, newPos);
                }
                Cache.Add(DiscPositions.ToList());
            }            
        }

        static void Main(string[] args)
        {
            foreach (string line in File.ReadLines("../../input.txt"))
            {
                string[] values = line.Split(' ');
                int numPos = int.Parse(values[3]);
                int currPos = int.Parse(values[11].TrimEnd(new char[] { '.' }));
                int startingPos = int.Parse(values[11].TrimEnd(new char[] { '.' }));
                Tuple<int, int, int> disc = Tuple.Create(numPos, startingPos, currPos);
                DiscPositions.Add(disc);
            }

            //DiscPositions.Add(Tuple.Create(11, 0, 0)); //Remove for Part 1

            while (!capsuleDropped)
            {
                time = prevTime + 1;
                ballPos = -1;
                calculatePositionsAtTime(time);
                running = true; //Button Click

                while (running)
                {
                    ballPos++;
                    time++;
                    calculatePositionsAtTime(time);
                    if(DiscPositions[ballPos].Item3 != 0)
                    {
                        running = false;
                    }
                    else if(ballPos == DiscPositions.Count - 1)
                    {
                        running = false;
                        capsuleDropped = true;
                    }
                }
                prevTime++;
            }

            Console.WriteLine("The First time to press the button is at time = " + (prevTime));
            Console.Read();
        }
    }
}

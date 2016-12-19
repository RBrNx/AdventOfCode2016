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
        static int time = 0;
        static List<Tuple<int, int>> DiscPositions = new List<Tuple<int, int>>();

        static bool isDiscOpen(Tuple<int, int> Disc, int time)
        {
            return (Disc.Item2 + time) % Disc.Item1 == 0;
        }

        static bool capsuleWillFall(int time)
        {
            for(int i = 0; i < DiscPositions.Count; i++)
            {
                if(!isDiscOpen(DiscPositions[i], i + time + 1))
                {
                    return false;
                }
            }
            return true;
        }

        static void Main(string[] args)
        {
            foreach (string line in File.ReadLines("../../input.txt"))
            {
                string[] values = line.Split(' ');
                int numPos = int.Parse(values[3]);
                int startingPos = int.Parse(values[11].TrimEnd(new char[] { '.' }));
                Tuple<int, int> disc = Tuple.Create(numPos, startingPos);
                DiscPositions.Add(disc);
            }

            DiscPositions.Add(Tuple.Create(11, 0)); //Remove for Part 1

            while (!capsuleWillFall(time))
            {
                time++;       
            }

            Console.WriteLine("The First time to press the button is at time = " + (time));
            Console.Read();
        }
    }
}

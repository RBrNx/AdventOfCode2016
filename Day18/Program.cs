using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    class Program
    {
        static List<List<char>> floor = new List<List<char>>();
        static char safe = '.';
        static char trap = '^';

        static bool isTrap(int y, int x)
        {
            char left = getTile(y - 1, x - 1);
            char center = getTile(y - 1, x);
            char right = getTile(y - 1, x + 1);

            if (left == trap && center == trap && right != trap){ return true; }
            else if (left != trap && center == trap && right == trap) { return true; }
            else if (left == trap && center != trap && right != trap) { return true; }
            else if (left != trap && center != trap && right == trap) { return true; }

            return false;
        }

        static char getTile(int y, int x)
        {
            if (x < 0 || x >= floor[0].Count || y < 0 || y >= floor.Count)
            {
                return '.';
            }
            return floor[y][x];
        }

        static void Main(string[] args)
        {
            string input = "^^^^......^...^..^....^^^.^^^.^.^^^^^^..^...^^...^^^.^^....^..^^^.^.^^...^.^...^^.^^^.^^^^.^^.^..^.^";
            List<char> currRow = input.ToList();
            int safeCount = 0; 
            floor.Add(currRow);

            int Part1 = 40;
            int Part2 = 400000;

            while(floor.Count < Part2)
            {
                StringBuilder row = new StringBuilder();
                for(int i = 0; i < currRow.Count; i++)
                {
                    row.Append(isTrap(floor.Count, i) ? '^' : '.');
                }
                floor.Add(row.ToString().ToList());
            }

            for(int i = 0; i < Part1; i++)
            {
                safeCount += string.Join("", floor[i].ToArray()).Split('.').Length - 1;
            }
            Console.WriteLine("Th number of Safe Tiles in " + Part1 + " rows is : " + safeCount);

            for (int i = Part1; i < Part2; i++)
            {
                safeCount += string.Join("", floor[i].ToArray()).Split('.').Length - 1;
            }
            Console.WriteLine("Th number of Safe Tiles in " + Part1 + " rows is : " + safeCount);
            Console.Read();
        }
    }
}

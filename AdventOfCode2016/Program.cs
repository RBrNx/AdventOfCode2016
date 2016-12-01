using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2016
{
    class Day1
    {
        static int x = 0; static int y = 0;
        static int vx = 0; static int vy = 0;
        static char direction = 'N';
        static string[] instructions;
        static bool duplicateFound = false;
        static List<Tuple<int, int>> visited = new List<Tuple<int, int>>();

        static void checkCoord(int x, int y)
        {
            if (!duplicateFound)
            {
                Tuple<int, int> coord = new Tuple<int, int>(x, y);
                duplicateFound = visited.Contains(coord);
                if (duplicateFound)
                {
                    vx = x; vy = y;
                }
                else
                {
                    visited.Add(coord);
                }
            }
        }

        static void Main(string[] args)
        {
            foreach (string line in File.ReadLines("../../input.txt"))
            {
                instructions = line.Split(new string[] { ", " }, StringSplitOptions.None);
                
                foreach(string inst in instructions)
                {
                    char turn = inst[0];
                    int distance = int.Parse(inst.Substring(1));

                    switch (direction)
                    {
                        case 'N':
                            if(turn == 'R')
                            {
                                direction = 'E';
                                for(int i = 0; i < distance; i++)
                                {
                                    x++;
                                    checkCoord(x, y);
                                }
                            }
                            else if(turn == 'L')
                            {
                                direction = 'W';
                                for (int i = 0; i < distance; i++)
                                {
                                    x--;
                                    checkCoord(x, y);
                                }
                            }
                            break;

                        case 'E':
                            if (turn == 'R')
                            {
                                direction = 'S';
                                for (int i = 0; i < distance; i++)
                                {
                                    y--;
                                    checkCoord(x, y);
                                }
                            }
                            else if (turn == 'L')
                            {
                                direction = 'N';
                                for (int i = 0; i < distance; i++)
                                {
                                    y++;
                                    checkCoord(x, y);
                                }
                            }
                            break;

                        case 'S':
                            if (turn == 'R')
                            {
                                direction = 'W';
                                for (int i = 0; i < distance; i++)
                                {
                                    x--;
                                    checkCoord(x, y);
                                }
                            }
                            else if (turn == 'L')
                            {
                                direction = 'E';
                                for (int i = 0; i < distance; i++)
                                {
                                    x++;
                                    checkCoord(x, y);
                                }
                            }
                            break;

                        case 'W':
                            if (turn == 'R')
                            {
                                direction = 'N';
                                for (int i = 0; i < distance; i++)
                                {
                                    y++;
                                    checkCoord(x, y);
                                }
                            }
                            else if (turn == 'L')
                            {
                                direction = 'S';
                                for (int i = 0; i < distance; i++)
                                {
                                    y--;
                                    checkCoord(x, y);
                                }
                            }
                            break;
                    }
                }

                Console.WriteLine("FinishCoord: (" + x + ", " + y + ")");
                Console.WriteLine("Manhatten Dist: " + (Math.Abs(x) + Math.Abs(y)));
                Console.WriteLine("----------------------------");
                Console.WriteLine("First DuplicateCoord: (" + vx + ", " + vy + ")");
                Console.WriteLine("Manhatten Dist: " + (Math.Abs(vx) + Math.Abs(vy)));
                Console.Read();
            }
        }
    }
}

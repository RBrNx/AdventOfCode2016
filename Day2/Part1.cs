using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    class Part1
    {
        static int kWidth = 3; static int kHeight = 3;
        static int[,] keypad;
        public static int currentKey = 5;
        public static List<int> combination = new List<int>();

        public static void createKeypad(int width, int height)
        {
            keypad = new int[width, height];

            for(int i = 1; i < width * height + 1; i++)
            {
                int x = (i - 1) / width;
                int y = (i - 1) - (x * height);

                keypad[x, y] = i;
            }
        }

        public static void checkBounds(char move)
        {
            int tempKey;
            int ty;

            switch (move)
            {
                case 'U':
                    tempKey = currentKey - kWidth;
                    if (tempKey > 0 && tempKey < kWidth * kHeight + 1)
                    {
                        currentKey = tempKey;
                    }
                    break;
                case 'D':
                    tempKey = currentKey + kWidth;
                    if (tempKey > 0 && tempKey < kWidth * kHeight + 1)
                    {
                        currentKey = tempKey;
                    }
                    break;
                case 'L':
                    tempKey = currentKey - 1;
                    ty = (currentKey - 1) / kWidth;
                    if (tempKey > ty * kWidth && tempKey < ty * kWidth + kWidth + 1)
                    {
                        currentKey = tempKey;
                    }
                    break;
                case 'R':
                    tempKey = currentKey + 1;
                    ty = (currentKey - 1) / kWidth;
                    if (tempKey > ty * kWidth && tempKey < ty * kWidth + kWidth + 1)
                    {
                        currentKey = tempKey;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    class Program
    {
        static void PartOne()
        {

            Part1.createKeypad(3, 3);

            foreach (string line in File.ReadLines("../../input.txt"))
            {
                foreach (char c in line)
                {
                    Part1.checkBounds(c);
                    Console.WriteLine("Move " + c + " to " + Part1.currentKey);
                }
                Part1.combination.Add(Part1.currentKey);
            }

            Console.WriteLine("---------------");
            Console.WriteLine("Combination is: ");
            foreach (int i in Part1.combination)
            {
                Console.Write(i);
            }
            Console.Read();
        }

        static void PartTwo()
        {
            Part2.createKeypad();

            foreach (string line in File.ReadLines("../../input.txt"))
            {
                foreach (char c in line)
                {
                    Part2.checkBounds(c);
                    Console.WriteLine("Move " + c + " to " + Part2.keypad[Part2.currPos.Item2, Part2.currPos.Item1]);
                }
                Part2.combination.Add(Part2.keypad[Part2.currPos.Item2, Part2.currPos.Item1]);
            }

            Console.WriteLine("---------------");
            Console.WriteLine("Combination is: ");
            foreach (char c in Part2.combination)
            {
                Console.Write(c);
            }
            Console.Read();
        }

        static void Main(string[] args)
        {
            //PartOne();
            PartTwo();
        }
    }
}

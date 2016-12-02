using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    class Part2
    {
        static int kWidth = 5; static int kHeight = 5;
        public static char[,] keypad;
        public static Tuple<int, int> currPos = new Tuple<int, int>(0, 2);
        public static List<char> combination = new List<char>();

        public static void createKeypad()
        {
            keypad = new char[,] { { ' ', ' ', '1', ' ', ' ' },
                                   { ' ', '2', '3', '4', ' ' },
                                   { '5', '6', '7', '8', '9' },
                                   { ' ', 'A', 'B', 'C', ' ' },
                                   { ' ', ' ', 'D', ' ', ' ' } };
        }

        public static void checkBounds(char move)
        {
            char tempChar;
            int x = currPos.Item1;
            int y = currPos.Item2;

            switch (move)
            {
                case 'U':
                    if(x >= 0 && x < kWidth && y - 1 >= 0 && y - 1 < kHeight)
                    {
                        tempChar = keypad[x, y - 1];
                        if (tempChar != ' ')
                        {
                            currPos = new Tuple<int, int>(x, y - 1);
                        }
                    }
                    break;
                case 'D':
                    if (x >= 0 && x < kWidth && y + 1 >= 0 && y + 1 < kHeight)
                    {
                        tempChar = keypad[x, y + 1];
                        if (tempChar != ' ')
                        {
                            currPos = new Tuple<int, int>(x, y + 1);
                        }
                    }
                    break;
                case 'L':
                    if (x - 1 >= 0 && x - 1 < kWidth && y >= 0 && y < kHeight)
                    {
                        tempChar = keypad[x - 1, y];
                        if (tempChar != ' ')
                        {
                            currPos = new Tuple<int, int>(x - 1, y);
                        }
                    }
                    break;
                case 'R':
                    if (x + 1 >= 0 && x + 1 < kWidth && y >= 0 && y < kHeight)
                    {
                        tempChar = keypad[x + 1, y];
                        if (tempChar != ' ')
                        {
                            currPos = new Tuple<int, int>(x + 1, y);
                        }
                    }
                    break;
            }
        }

    }
}

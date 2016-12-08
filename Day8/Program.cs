using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    class Program
    {
        static int LCDWidth = 50;
        static int LCDHeight = 6;
        static char[][] tinyLCD = Enumerable.Range(0, LCDHeight).Select(x => new char[LCDWidth]).ToArray();
        static int LitPixelCount = 0;

        static void Rect(int A, int B)
        {
            for(int y = 0; y < B; y++)
            {
                for(int x = 0; x < A; x++)
                {
                    tinyLCD[y][x] = '#';
                }
            }
        }

        static void RotateRow(int A, int B)
        {
            for(int i = 0; i < B; i++)
            {
                char[] row = Enumerable.Range(0, LCDWidth).Select(a => tinyLCD[A][a]).ToArray();
                Array.Copy(row, 0, tinyLCD[A], 1, row.Length - 1);
                tinyLCD[A][0] = row[row.Length - 1];
            }
        }

        static void RotateCol(int A, int B)
        {
            char[] col = Enumerable.Range(0, LCDHeight).Select(a => tinyLCD[a][A]).ToArray();
            char[] shiftedCol = new char[col.Length];

            for(int i = 0; i < B; i++)
            {
                Array.Copy(col, 0, shiftedCol, 1, col.Length - 1);
                shiftedCol[0] = col[col.Length - 1];
                Array.Copy(shiftedCol, col, col.Length);
            }

            for(int i = 0; i < shiftedCol.Length; i++)
            {
                tinyLCD[i][A] = shiftedCol[i];
            }
        }

        static void displayLCD(char[][] LCD)
        {
            for(int y = 0; y < LCD.Length; y++)
            {
                for(int x = 0; x < LCD[y].Length; x++)
                {
                    Console.Write(LCD[y][x]);
                }
                Console.WriteLine("");
            }
        }

        static void Main(string[] args)
        {
            foreach(string line in File.ReadLines("../../input.txt"))
            {
                string[] instruction = line.Split(' ');
                switch (instruction[0])
                {
                    case "rect":
                        int[] values = instruction[1].Split('x').Select(int.Parse).ToArray();
                        Rect(values[0], values[1]);
                        break;
                    case "rotate":
                        if(instruction[1] == "row")
                        {
                            RotateRow(int.Parse(instruction[2].Substring(2)), int.Parse(instruction[4]));
                        }
                        else if(instruction[1] == "column")
                        {
                            RotateCol(int.Parse(instruction[2].Substring(2)), int.Parse(instruction[4]));
                        }
                        break;
                    default:
                        break;
                }

                Console.WriteLine(line);
                displayLCD(tinyLCD);
                Console.WriteLine("\n");
            }

            for(int y = 0; y < tinyLCD.Length; y++)
            {
                for(int x = 0; x < tinyLCD[y].Length; x++)
                {
                    if(tinyLCD[y][x] == '#')
                    {
                        LitPixelCount++;
                    }
                }
            }

            Console.WriteLine("The number of Lit Pixels is: " + LitPixelCount);
            Console.Read();
        }
    }
}

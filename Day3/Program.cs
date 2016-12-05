using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Day3
{
    class Program
    {
        static bool validTriangle(int a, int b, int c)
        {
            bool index1 = a < b + c;
            bool index2 = b < c + a;
            bool index3 = c < a + b;

            if (index1 && index2 && index3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            int validCountPart1 = 0;
            int validCountPart2 = 0;

            foreach (string set in File.ReadLines("../../input.txt"))
            {
                string trimmed = set.TrimStart(' ');
                int[] indices = Array.ConvertAll(Regex.Split(trimmed, @"\s+"), int.Parse);

                if(validTriangle(indices[0], indices[1], indices[2]))
                {
                    validCountPart1++;
                }
            }

            string[] lines = File.ReadAllLines("../../input.txt");
            int[] split1; int[] split2; int[] split3;

            for(int i = 0; i < lines.Length; i += 3)
            {
                lines[i] = lines[i].TrimStart(' ');
                lines[i + 1] = lines[i + 1].TrimStart(' ');
                lines[i + 2] = lines[i + 2].TrimStart(' ');

                split1 = Array.ConvertAll(Regex.Split(lines[i], @"\s+"), int.Parse);
                split2 = Array.ConvertAll(Regex.Split(lines[i + 1], @"\s+"), int.Parse);
                split3 = Array.ConvertAll(Regex.Split(lines[i + 2], @"\s+"), int.Parse);

                for (int j = 0; j < 3; j++)
                {
                    if(validTriangle(split1[j], split2[j], split3[j]))
                    {
                        validCountPart2++;
                    }
                }
            }

            Console.WriteLine("Number of Valid Triangles in Part 1: " + validCountPart1);
            Console.WriteLine("Number of Valid Triangles in Part 2: " + validCountPart2);
            Console.Read();
        }
    }
}

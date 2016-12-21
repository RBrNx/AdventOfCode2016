using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    class Program
    {
        static string swapPosition(string input, int x, int y)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < input.Length; i++)
            {
                if (i == x) sb.Append(input[y]);
                else if (i == y) sb.Append(input[x]);
                else sb.Append(input[i]);
            }
            return sb.ToString();
        }

        static string swapLetter(string input, char x, char y)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == x) sb.Append(y);
                else if (input[i] == y) sb.Append(x);
                else sb.Append(input[i]);
            }
            return sb.ToString();
        }

        static string rotate(string input, bool left, int steps)
        {
            StringBuilder sb = new StringBuilder(input);
            for(int i = 0; i < steps; i++)
            {
                if (left)
                {
                    char front = sb[0];
                    sb.Remove(0, 1);
                    sb.Append(front);
                }
                else
                {
                    char back = sb[sb.Length - 1];
                    sb.Remove(sb.Length - 1 , 1);
                    sb.Insert(0, back);
                }
            }
            return sb.ToString();
        }

        static string rotateBasedPosition(string input, char x)
        {
            int index = input.IndexOf(x);
            string output = "";
            if(index > -1)
            {
                if(index >= 4) { index++; }
                output = rotate(input, false, index + 1);
            }
            return output;
        }

        static string findOrigPosition(string input, char x)
        {
            for(int i = 0; i < input.Length; i++)
            {
                string output = rotate(input, true, i);
                if(rotateBasedPosition(output, x) == input)
                {
                    return output;
                }
            }
            return "";
        }

        static string reverse(string input, int x, int y)
        {
            string reversedLetters = new string(input.Substring(x, y - x + 1).ToCharArray().Reverse().ToArray());
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < input.Length; i++)
            {
                if(i == x)
                {
                    sb.Append(reversedLetters);
                    i = y;
                }
                else { sb.Append(input[i]); }
            }
            return sb.ToString();

        }

        static string move(string input, int x, int y)
        {
            StringBuilder sb = new StringBuilder();
            char toRemove = input[x];
            for(int i = 0; i < input.Length; i++)
            {
                if (i != x) { sb.Append(input[i]); }
            }
            sb.Insert(y, toRemove);
            return sb.ToString();
        }

        static void Main(string[] args)
        {
            string unscrambled = "abcdefgh";
            string scrambled = unscrambled;
            string[] operations = File.ReadAllLines("../../input.txt");

            string y = findOrigPosition("fbgdceah", 'f');
            string x = rotateBasedPosition(y, 'f');

            foreach (string line in operations)
            {
                string operation = line.Split(' ')[0];
                string detail = line.Split(' ')[1];
                string[] values = line.Split(' ');

                switch (operation)
                {
                    case "swap":
                        if(detail == "position") { scrambled = swapPosition(scrambled, int.Parse(values[2]), int.Parse(values[5])); }
                        else if(detail == "letter") { scrambled = swapLetter(scrambled, Convert.ToChar(values[2]), Convert.ToChar(values[5])); }
                        break;
                    case "move":
                        scrambled = move(scrambled, int.Parse(values[2]), int.Parse(values[5]));
                        break;
                    case "reverse":
                        scrambled = reverse(scrambled, int.Parse(values[2]), int.Parse(values[4]));
                        break;
                    case "rotate":
                        if (detail == "based") { scrambled = rotateBasedPosition(scrambled, Convert.ToChar(values[6])); }
                        else if (detail == "left") { scrambled = rotate(scrambled, true, int.Parse(values[2])); }
                        else if (detail == "right") { scrambled = rotate(scrambled, false, int.Parse(values[2])); }
                        break;
                }
            }

            Console.WriteLine("The scrambled password of " + unscrambled + " is " + scrambled);

            scrambled = "fbgdceah";
            unscrambled = scrambled;
            operations = operations.Reverse().ToArray();

            foreach (string line in operations)
            {
                string operation = line.Split(' ')[0];
                string detail = line.Split(' ')[1];
                string[] values = line.Split(' ');

                switch (operation)
                {
                    case "swap":
                        if (detail == "position") { unscrambled = swapPosition(unscrambled, int.Parse(values[5]), int.Parse(values[2])); }
                        else if (detail == "letter") { unscrambled = swapLetter(unscrambled, Convert.ToChar(values[5]), Convert.ToChar(values[2])); }
                        break;
                    case "move":
                        unscrambled = move(unscrambled, int.Parse(values[5]), int.Parse(values[2]));
                        break;
                    case "reverse":
                        unscrambled = reverse(unscrambled, int.Parse(values[2]), int.Parse(values[4]));
                        break;
                    case "rotate":
                        if (detail == "based") { unscrambled = findOrigPosition(unscrambled, Convert.ToChar(values[6])); }
                        else if (detail == "left") { unscrambled = rotate(unscrambled, false, int.Parse(values[2])); }
                        else if (detail == "right") { unscrambled = rotate(unscrambled, true, int.Parse(values[2])); }
                        break;
                }
            }

            Console.WriteLine("The scrambled password of " + scrambled + " is " + unscrambled);
            Console.Read();
        }
    }
}

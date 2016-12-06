using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    class Program
    {
        static List<Dictionary<char, int>> letterCounts = Enumerable.Range(0, 8).Select(i => new Dictionary<char, int>()).ToList();
        static string mostCommonHiddenMessage = "";
        static string leastCommonHiddenMessage = "";

        static void addToDictionary(int i, char c)
        {
            if (letterCounts[i].ContainsKey(c))
            {
                letterCounts[i][c]++; //Increment Count of c in Dictionary i
            }
            else
            {
                letterCounts[i].Add(c, 1);
            }
        }

        static void Main(string[] args)
        {
            foreach (string line in File.ReadLines("../../input.txt"))
            {
                for(int i = 0; i < line.Length; i++)
                {
                    addToDictionary(i, line[i]);
                }
            }

            for(int i = 0; i < letterCounts.Count; i++)
            {
                var mostCommon = letterCounts[i].OrderByDescending(pair => pair.Value).Take(1).ToList();
                mostCommonHiddenMessage += mostCommon[0].Key;

                var leastCommon = letterCounts[i].OrderBy(pair => pair.Value).Take(1).ToList();
                leastCommonHiddenMessage += leastCommon[0].Key;
            }

            Console.WriteLine("The Most Common error-corrected message is: " + mostCommonHiddenMessage);
            Console.WriteLine("The Most Common error-corrected message is: " + leastCommonHiddenMessage);
            Console.Read();
            
        }
    }
}

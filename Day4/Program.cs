using System;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    class Program
    {
        static string createRegexPattern(int dashCount)
        {
            string pattern = @"";

            for(int i = 0; i < dashCount; i++)
            {
                pattern += @"(\w+)-";
            }
            pattern += @"(\d+)(\[\w+\])";

            return pattern;
        }

        static bool compareChecksum(List<KeyValuePair<char, int>> letters, string checksum)
        {
            string check = "";
            List<char> toSort = new List<char>();

            for(int i = 0; i < letters.Count; i++)
            {
                check += letters[i].Key;
            }

            checksum = checksum.Trim(new char[] { '[', ']' });

            if(check == checksum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void decryptRoom(int sectorID, string encrypted, out string decrypted)
        {
            char currentChar;
            string decryption = "";

            for(int i = 0; i < encrypted.Length; i++)
            {
                currentChar = encrypted[i];

                if (currentChar == '-')
                {
                    decryption += ' ';
                }
                else
                {
                    for (int j = 0; j < sectorID; j++)
                    {
                        if (currentChar == 'z')
                        {
                            currentChar = 'a';
                        }
                        else
                        {
                            currentChar++;
                        }
                    }
                    decryption += currentChar;
                }              
            }

            decrypted = decryption;
        }

        static void Main(string[] args)
        {
            int sectorIDSum = 0;

            foreach (string line in File.ReadLines("../../input.txt"))
            {
                int dashCount = line.Split('-').Length - 1;
                Regex pattern = new Regex(createRegexPattern(dashCount));
                Match m = pattern.Match(line);

                if (m.Success)
                {
                    Dictionary<char, int> letterCounts = new Dictionary<char, int>();
                    string roomName = "";
                    for(int i = 1; i < dashCount + 1; i++)
                    {
                        roomName += m.Groups[i].Value + "-";
                    }

                    for(int i = 0; i < roomName.Length; i++)
                    {
                        char key = roomName[i];
                        if(key == '-')
                        {
                            //Do nothing
                        }
                        else if (letterCounts.ContainsKey(key))
                        {
                            letterCounts[key]++;
                        }
                        else
                        {
                            letterCounts.Add(key, 1);
                        }
                    }

                    var top5 = letterCounts.OrderByDescending(pair => pair.Value).ThenBy(pair => pair.Key).Take(5).ToList();

                    if(compareChecksum(top5, m.Groups[dashCount + 2].Value))
                    {
                        int sectorID = int.Parse(m.Groups[dashCount + 1].Value);
                        sectorIDSum += sectorID;
                        string decrypted = "";
                        decryptRoom(sectorID, roomName, out decrypted);

                        if (decrypted.Contains("northpole"))
                        {
                            Console.WriteLine(roomName + "--" + sectorID + "--" + decrypted);
                        }
                    }
                }
            }

            Console.WriteLine("SectorID Sum is: " + sectorIDSum);
            Console.Read();
        }
    }
}

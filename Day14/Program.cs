using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    class Program
    {
        static List<string> hashes = new List<string>();
        static List<string> keys = new List<string>();

        static string CalculateHash(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

        static bool findConsecutive(string hash, int length, out string consecutive)
        {
            for(int i = 0; i < hash.Length - 2; i++)
            {
                if(hash[i] == hash[i + 1] && hash[i] == hash[i + 2])
                {
                    consecutive = new string(new char[] { hash[i], hash[i + 1], hash[i + 2] });
                    return true;
                }
            }

            consecutive = "";
            return false;
        }

        static bool findFive(string hash, char c)
        {
            if (hash.Contains(new string(new char[] { c, c, c, c, c })))
            {
                return true;
            }

            return false;
        }

        static string stretchKey(string hash)
        {
            string newHash = hash;
            for(int i = 0; i < 2016; i++)
            {
                newHash = CalculateHash(newHash);
            }
            return newHash;
        }

        static void Main(string[] args)
        {
            string salt = "cuanljph";
            int index = 0;
            int loopCount = 0;
            string consecutive = "";
            bool keyStretching = true; //Part 1 = False, Part 2 = True

            string indexedSalt = salt + index;
            string hash = CalculateHash(indexedSalt);
            hash = stretchKey(hash);
            hashes.Add(hash);

            while (keys.Count < 64)
            {
                if (hashes.Count() < loopCount + 1 + 1000)
                {
                    int difference = (loopCount + 1 + 1000) - hashes.Count();
                    for (int i = 0; i < difference; i++)
                    {
                        index++;
                        indexedSalt = salt + index;
                        hash = CalculateHash(indexedSalt);
                        if (keyStretching)
                        {
                            hash = stretchKey(hash);
                        }
                        hashes.Add(hash);
                    }
                }

                if (findConsecutive(hashes[loopCount], 3, out consecutive))
                {
                    char c = consecutive[0];
                    for(int i = loopCount + 1; i < loopCount + 1 + 1000; i++)
                    {
                        if(findFive(hashes[i], c))
                        {
                            keys.Add(hashes[loopCount]);
                            break;
                        }
                    }
                }
                loopCount++;
            }

            Console.WriteLine("The index that produced the 64th Hash is: " + (loopCount - 1)); //-1 Becaye loopCount is increased at the end of the while loop, so produces a value too high
            Console.Read();
        }
    }
}

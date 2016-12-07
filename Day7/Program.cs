using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    class Program
    {
        static int TLSIPCount = 0;
        static int SSLIPCount = 0;

        static bool ABBATest(string sequence)
        {
            for(int i = 0; i < sequence.Length - 3; i++)
            {
                char two = sequence[i + 1];
                char three = sequence[i + 2];

                if(two == three)
                {
                    char one = sequence[i];
                    char four = sequence[i + 3];
                    if(one == four && one != two)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static void Part1()
        {

            foreach (string line in File.ReadLines("../../input.txt"))
            {
                List<string> sequences = new List<string>();
                List<string> bracketSequences = new List<string>();
                bool supportsTLS = true;
                int numBrackets = line.Split('[').Length - 1;
                string modifyLine = line;

                for (int i = 0; i < numBrackets; i++)
                {
                    int lbloc = modifyLine.IndexOf('[');
                    int rbloc = modifyLine.IndexOf(']');

                    sequences.Add(modifyLine.Substring(0, lbloc));
                    bracketSequences.Add(modifyLine.Substring(lbloc + 1, rbloc - lbloc - 1));

                    modifyLine = modifyLine.Remove(0, lbloc);
                    modifyLine = modifyLine.Remove(0, modifyLine.IndexOf(']') + 1);
                }
                sequences.Add(modifyLine);

                for (int i = 0; i < bracketSequences.Count; i++)
                {
                    if (ABBATest(bracketSequences[i]))
                    {
                        supportsTLS = false;
                    }
                }

                if (supportsTLS)
                {
                    supportsTLS = false;

                    for (int i = 0; i < sequences.Count; i++)
                    {
                        if (ABBATest(sequences[i]))
                        {
                            supportsTLS = true;
                        }
                    }

                    if (supportsTLS) TLSIPCount++;
                }
            }
        }

        static void GetABAs(List<string> inputList, List<string> ABAStorage)
        {
            for(int i = 0; i < inputList.Count; i++)
            {
                for(int j = 0; j < inputList[i].Length - 2; j++)
                {
                    char one = inputList[i][j];
                    char two = inputList[i][j + 1];
                    char three = inputList[i][j + 2];

                    if(one == three && one != two)
                    {
                        ABAStorage.Add(new string(new char[] { one, two, three }));
                    }
                }
            }
        }

        static string GetBAB(string ABA)
        {
            return new string(new char[] { ABA[1], ABA[0], ABA[1] });
        }

        static void Part2()
        {
            foreach (string line in File.ReadLines("../../input.txt"))
            {
                List<string> sequences = new List<string>();
                List<string> bracketSequences = new List<string>();
                List<string> ABAs = new List<string>();
                bool supportsSSL = false;

                int numBrackets = line.Split('[').Length - 1;
                string modifyLine = line;

                for (int i = 0; i < numBrackets; i++)
                {
                    int lbloc = modifyLine.IndexOf('[');
                    int rbloc = modifyLine.IndexOf(']');

                    sequences.Add(modifyLine.Substring(0, lbloc));
                    bracketSequences.Add(modifyLine.Substring(lbloc + 1, rbloc - lbloc - 1));

                    modifyLine = modifyLine.Remove(0, lbloc);
                    modifyLine = modifyLine.Remove(0, modifyLine.IndexOf(']') + 1);
                }
                sequences.Add(modifyLine);

                GetABAs(sequences, ABAs);

                for(int i = 0; i < ABAs.Count; i++)
                {
                    if (supportsSSL) break;
                    string BAB = GetBAB(ABAs[i]);

                    for(int j = 0; j < bracketSequences.Count; j++)
                    {
                        if (bracketSequences[j].Contains(BAB))
                        {
                            supportsSSL = true;
                            SSLIPCount++;
                            break;
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Part1();
            Part2();
 
            Console.WriteLine("The amount of IPs that support TLS is: " + TLSIPCount);
            Console.WriteLine("The amount of IPs that support SSL is: " + SSLIPCount);
            Console.Read();
        }
    }
}

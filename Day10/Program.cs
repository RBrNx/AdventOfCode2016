using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{

    class Program
    {
        struct Bot
        {
            public int low;
            public int high;
            public int destLow;
            public int destHigh;
            public bool outputLow;
            public bool outputHigh;

            public Bot(int l, int h, int dL, int dH, bool oL, bool oH)
            {
                low = l;
                high = h;
                destLow = dL;
                destHigh = dH;
                outputLow = oL;
                outputHigh = oH;
            }
        }

        static Bot[] Bots = Enumerable.Range(0, 300).Select(b => new Bot(-1, -1, -1, -1, false, false)).ToArray();
        static int[] Output = new int[30];
        static List<Bot> readyBots = new List<Bot>();
        static List<int> readyBotsNum = new List<int>();

        static void PassMicrochip(int n, int dest, bool output)
        {
            if (output)
            {
                Output[dest] += n;
                //Console.WriteLine(n + " added to Output " + dest);
            }
            else
            {
                List<int> numbers = new List<int>() { Bots[dest].high, Bots[dest].low, n };
                numbers.Sort();

                Bots[dest].high = numbers[2];
                Bots[dest].low = numbers[1];
                //Console.WriteLine(n + " given to bot " + dest);
            }          
        }

        static void checkForTwoChips()
        {
            for(int i = 0; i < Bots.Length; i++)
            {
                if(Bots[i].high > -1 && Bots[i].low > -1 && Bots[i].destHigh > -1 && Bots[i].destLow > -1)
                {
                    if(Bots[i].high == 61 && Bots[i].low == 17)
                    {
                        Console.WriteLine("The bot that compares 61 and 17 is Bot: " + i);
                    }
                    readyBots.Add(Bots[i]);
                    readyBotsNum.Add(i);
                }
            }
        }

        static void Main(string[] args)
        {
            List<string> lines = File.ReadLines("../../input.txt").ToList();
            lines = lines.OrderByDescending(o => o[0]).ToList();

            foreach(string line in lines)
            {
                if (line.StartsWith("bot"))
                {
                    string[] instructions = line.Split(' ');
                    int BotN = int.Parse(instructions[1]);

                    if(instructions[5] == "output")
                    {
                        int OutLow = int.Parse(instructions[6]);
                        Bots[BotN].destLow = OutLow;
                        Bots[BotN].outputLow = true;
                    }
                    else
                    {
                        int BotLow = int.Parse(instructions[6]);
                        Bots[BotN].destLow = BotLow;
                        Bots[BotN].outputLow = false;
                    }

                    if (instructions[10] == "output")
                    {
                        int OutHigh = int.Parse(instructions[11]);
                        Bots[BotN].destHigh = OutHigh;
                        Bots[BotN].outputHigh = true;
                    }
                    else
                    {
                        int BotHigh = int.Parse(instructions[11]);
                        Bots[BotN].destHigh = BotHigh;
                        Bots[BotN].outputHigh = false;
                    }
                }
                else if (line.StartsWith("value"))
                {
                    string[] instructions = line.Split(' ');
                    int value = int.Parse(instructions[1]);
                    int bot = int.Parse(instructions[5]);
                    PassMicrochip(value, bot, false);
                }

                checkForTwoChips();
                while (readyBots.Count() > 0)
                {
                    for(int i = readyBots.Count - 1; i > -1; i--)
                    {
                        PassMicrochip(readyBots[i].low, readyBots[i].destLow, readyBots[i].outputLow);
                        Bots[readyBotsNum[i]].low = -2;
                        PassMicrochip(readyBots[i].high, readyBots[i].destHigh, readyBots[i].outputHigh);
                        Bots[readyBotsNum[i]].high = -2;

                        readyBots.RemoveAt(i);
                        readyBotsNum.RemoveAt(i);
                    }
                    checkForTwoChips();
                }
                
            }

            Console.WriteLine("Multiplying Outputs 0, 1 and 2 = " + Output[0] * Output[1] * Output[2]);
            Console.Read();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    [DebuggerDisplay("Pos = {position} Pres = {presents}")]
    struct Elf
    {
        public int position;
        public int presents;

        public Elf(int pos, int pres)
        {
            position = pos;
            presents = pres;
        }
    }

    class Program
    {
        //With thanks to /u/caeonosphere for Part 2
        static int Part2(int n)
        {
            int pow = (int)Math.Floor(Math.Log(n) / Math.Log(3));
            int b = (int)Math.Pow(3, pow);
            if (n == b)
                return n;
            if (n - b <= b)
                return n - b;
            return 2 * n - 3 * b;
        }

        static void Main(string[] args)
        {
            int input = 3014387;
            List<Elf> elves = Enumerable.Range(0, input).Select(x => new Elf(x, 1)).ToList();

            while (!elves.Any(e => e.presents == input))
            {
                for(int i = 0; i < elves.Count; i++)
                {
                    if(elves[i].presents > 0)
                    {
                        if (i < elves.Count - 1)
                        {
                            int elfNum = elves[i].position;
                            Elf newElf = new Elf(elfNum, elves[i].presents + elves[i + 1].presents);
                            elves[i] = newElf;
                            elves.RemoveAt(i + 1);
                        }
                        else if (i == elves.Count - 1)
                        {
                            int elfNum = elves[i].position;
                            Elf newElf = new Elf(elfNum, elves[i].presents + elves[0].presents);
                            elves[i] = newElf;
                            elves.RemoveAt(0);
                        }
                    }   
                }
            }

            Console.WriteLine("The greedy elf for Part 1 is #" + (elves[0].position + 1) + " who has " + elves[0].presents + " presents");
            Console.WriteLine("The greedy elf for Part 2 is #" + Part2(input));
            Console.Read();
        }
    }
}

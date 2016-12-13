using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    class Program
    {
        static Dictionary<string, int> Registers = new Dictionary<string, int>() { { "a", 0 }, { "b", 0 }, { "c", 1 }, { "d", 0 } }; // c = 0 Part 1, c = 1 Part 2

        static void copy(string x, string y)
        {
            int n;
            bool isNum = int.TryParse(x, out n);
            if (isNum)
            {
                //Console.WriteLine("Copy " + x + " to " + y);
                Registers[y] = n;
            }
            else
            {
                //Console.WriteLine("Copy " + x + " to " + y);
                Registers[y] = Registers[x];
            }
        }

        static void increment(string x)
        {
            //Console.WriteLine("Increment " + x);
            Registers[x] += 1;
        }

        static void decrement(string x)
        {
            //Console.WriteLine("Decrement " + x);
            Registers[x] -= 1;
        }

        static int jump(string x, string y)
        {
            int n;
            bool isNum = int.TryParse(x, out n);
            if (!isNum)
            {
                if(Registers[x] != 0)
                {
                    //Console.WriteLine("Jump " + y);
                    return int.Parse(y);
                }
                else
                {
                    //Console.WriteLine("Jump ignored");
                    return 1;
                }             
            }
            else
            {
                if (n != 0)
                {
                    //Console.WriteLine("Jump " + y);
                    return int.Parse(y);
                }
                else
                {
                    //Console.WriteLine("Jump ignored");
                    return 1;
                }
            }
        }



        static void Main(string[] args)
        {
            string[] instructions = File.ReadAllLines("../../input.txt");

            for (int i = 0; i < instructions.Length;)
            {
                string[] instruc = instructions[i].Split(' ');
                switch (instruc[0])
                {
                    case "cpy":
                        copy(instruc[1], instruc[2]);
                        i++;
                        break;
                    case "inc":
                        increment(instruc[1]);
                        i++;
                        break;
                    case "dec":
                        decrement(instruc[1]);
                        i++;
                        break;
                    case "jnz":
                        i += jump(instruc[1], instruc[2]);
                        break;
                }

            }

            Console.WriteLine("Register A: " + Registers["a"] + "   Register B: " + Registers["b"] + "   Register C: " + Registers["c"] + "   Register D: " + Registers["d"]);
            Console.Read();
        }
    }
}

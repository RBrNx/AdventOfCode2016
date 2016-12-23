using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    class Program
    {
        static Dictionary<string, int> Registers = new Dictionary<string, int>() { { "a", 12 }, { "b", 0 }, { "c", 0 }, { "d", 0 } };
        static string[] instructions = File.ReadAllLines("../../input2.txt");

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
                if (Registers[x] != 0)
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
                    int m;
                    isNum = int.TryParse(y, out m);
                    if (isNum) return m;
                    else return Registers[y];
                }
                else
                {
                    //Console.WriteLine("Jump ignored");
                    return 1;
                }
            }
        }

        static void toggle(string x, int i)
        {
            int n;
            string instruction = "";
            bool isNum = int.TryParse(x, out n);
            if (isNum) {
                if(n >= instructions.Length) { return; }
                instruction = instructions[n];
            }
            else {
                if (i + Registers[x] >= instructions.Length) { return; }
                instruction = instructions[i + Registers[x]];
            }

            string[] instruc = instruction.Split(' ');
            switch (instruc[0])
            {
                case "inc":
                    instruction = "dec " + instruc[1];
                    break;
                case "dec":
                    instruction = "inc " + instruc[1];
                    break;
                case "tgl":
                    instruction = "inc " + instruc[1];
                    break;
                case "jnz":
                    instruction = "cpy " + instruc[1] + " " + instruc[2];
                    break;
                case "cpy":
                    instruction = "jnz " + instruc[1] + " " + instruc[2];
                    break;
                default:
                    instruction = "";
                    break;
            }

            if (isNum) { instructions[n] = instruction; }
            else { instructions[i + Registers[x]] = instruction; }
        }

        static void multiply(string x, string y, string z)
        {
            int mulitple = Registers[x] * Registers[y];
            Registers[z] = mulitple;
        }

        static void Main(string[] args)
        {
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
                    case "tgl":
                        toggle(instruc[1], i);
                        i++;
                        break;
                    case "mul":
                        multiply(instruc[1], instruc[2], instruc[3]);
                        i++;
                        break;
                }

            }

            Console.WriteLine("Register A: " + Registers["a"] + "   Register B: " + Registers["b"] + "   Register C: " + Registers["c"] + "   Register D: " + Registers["d"]);
            Console.Read();
        }
    }
}

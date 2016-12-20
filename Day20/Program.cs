using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    class Program
    {        

        static long Part1(List<Tuple<long, long>> ipAddresses)
        {
            long used = 0;
            for (int i = 0; i < ipAddresses.Count; i++)
            {
                if (ipAddresses[i].Item1 <= used + 1)
                {
                    used = Math.Max(ipAddresses[i].Item2, used);
                }
                else
                {
                    return used + 1;
                }
            }
            return -1;
        }

        static long Part2(List<Tuple<long, long>> ipAddresses)
        {
            long used = 0;
            long count = 0;
            long max = 4294967295;
            for (int i = 0; i < ipAddresses.Count; i++)
            {
                if (ipAddresses[i].Item1 > used + 1)
                {
                    count += ipAddresses[i].Item1 - used - 1;
                }
                used = Math.Max(ipAddresses[i].Item2, used);
            }
            count += max - used;
            return count;
        }

        static void Main(string[] args)
        {
            List<Tuple<long, long>> ipAddresses = new List<Tuple<long, long>>();

            foreach(string line in File.ReadLines("../../input.txt"))
            {
                long[] range = line.Split('-').Select(long.Parse).ToArray();
                ipAddresses.Add(Tuple.Create(range[0], range[1]));
            }

            ipAddresses.Sort((ip1, ip2) => ip1.Item1.CompareTo(ip2.Item1));

            Console.WriteLine("The lowest valid IP is: " + Part1(ipAddresses));
            Console.WriteLine("The number of valid IPs is: " + Part2(ipAddresses));
            Console.Read();
        }
    }
}

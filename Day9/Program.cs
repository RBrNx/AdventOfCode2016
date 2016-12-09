using System;
using System.IO;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9
{
    class Program
    {
        static BigInteger decompress(string input, int n)
        {
            BigInteger count = 0;
            for(int i = 0; i < input.Length; i++)
            {
                if(input[i] == '(')
                {
                    string marker = new string(input.Skip(i + 1).TakeWhile(ch => ch != ')').ToArray());
                    var arr = marker.Split('x');
                    int nchars = int.Parse(arr[0]);
                    int skip = i + marker.Length + 2;
                    count += nchars * int.Parse(arr[1]); //Part 1
                    //count += decompress(input.Skip(skip).Take(nchars).ToString(), int.Parse(arr[1])); //Part 2
                    i = skip + nchars - 1;
                }
                else
                {
                    count++;
                }
            }
            return count * n;
        }

        static void Main(string[] args)
        {
            string compressed = File.ReadAllText("../../input.txt");
           
            Console.WriteLine("The Length of the uncompressed string is: " + decompress(compressed, 1));
            Console.Read();
        }
    }
}

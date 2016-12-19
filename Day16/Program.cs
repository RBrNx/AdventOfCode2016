using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    class Program
    {
        static string dragonCurve(string input)
        {
            string a = input;
            string b = a;
            b = new string(b.ToCharArray().Reverse().ToArray());
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < b.Length; i++)
            {
                if(b[i] == '0') { sb.Append('1'); }
                else if(b[i] == '1') { sb.Append('0'); }
            }
            b = sb.ToString();
            return a + "0" + b;
        }

        static string generateChecksum(string input)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < input.Length - 1; i += 2)
            {
                if(input[i] == input[i + 1]) { sb.Append('1'); }
                else { sb.Append('0'); }
            }
            return sb.ToString();
        }

        static string CalculateCorrectChecksum(string input, int diskLength)
        {
            string checksum = "";

            while (input.Length < diskLength)
            {
                input = dragonCurve(input);
            }

            checksum = input.Substring(0, diskLength);
            do
            {
                checksum = generateChecksum(checksum);
            }
            while (checksum.Length % 2 == 0);

            return checksum;
        }

        static void Main(string[] args)
        {
            string input = "11100010111110100";
            Console.WriteLine("First Disk Checksum is: " + CalculateCorrectChecksum(input, 272));
            Console.WriteLine("Second Disk Checksum is: " + CalculateCorrectChecksum(input, 35651584));
            Console.Read();
        }
    }
}

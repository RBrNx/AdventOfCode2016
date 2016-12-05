using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Day5
{
    class Program
    {
        static string input = "abbhdwsy";
        static string password = "";
        static int index = 0;

        static string CalculateHash(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        static void Part1()
        {
            index = 0;
            while (password.Length < 8)
            {
                string hash = CalculateHash(input + index);

                if (hash.StartsWith("00000"))
                {
                    password += hash[5];
                    string coolPassword = password;
                    for (int i = password.Length; i < 8; i++)
                    {
                        coolPassword += '_';
                    }
                    Console.WriteLine(coolPassword + "\t\t" + "Hash: " + hash.Substring(0, 6) + " from input: " + (input + index));
                }

                index++;
            }
        }

        static void Part2()
        {
            index = 0;
            StringBuilder tempPassword = new StringBuilder("--------");
            int digitsFound = 0;

            while (digitsFound < 8)
            {
                string hash = CalculateHash(input + index);

                if (hash.StartsWith("00000"))
                {
                    int pos;
                    if(int.TryParse(hash[5].ToString(), out pos) && pos >= 0 && pos < 8 && tempPassword[pos] == '-')
                    {
                        tempPassword[pos] = hash[6];
                        Console.WriteLine(tempPassword.ToString() + "\t\t" + "Hash: " + hash.Substring(0, 7) + " from input: " + (input + index));
                        digitsFound++;
                    }                   
                }
                index++;
            }

            password = tempPassword.ToString();
        }

        static void Main(string[] args)
        {
            //Part1();
            Part2();  

            Console.WriteLine("Door Password is: " + password);
            Console.Read();
        }
    }
}

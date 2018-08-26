using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static System.Console;

namespace Hashing
{
    class Program
    {
        static SHA256Managed crypt = new SHA256Managed();
        static byte[] shaInput = new byte[256];
        static Random random = new Random();
        static StringBuilder hashString = new StringBuilder();

        static void Main(string[] args)
        {
            var maxLeadingZeros = 0;

            for (int i = 0; i < 1000000; i++)
            {
                var hash = Hash();

                if (hash.LeadingZeros > maxLeadingZeros)
                {
                    maxLeadingZeros = hash.LeadingZeros;
                    WriteLine("Leading Zeros: {0}", hash.LeadingZeros);
                    PrintHash(hash.Binary);
                }
            }
        }

        private static void PrintHash(string binary)
        {
            WriteLine(string.Join(Environment.NewLine, Split(binary, 32)));
        }

        static (string Binary, int LeadingZeros) Hash()
        {
            random.NextBytes(shaInput);
            var hash = crypt.ComputeHash(shaInput);

            hashString.Clear();
            foreach (var @byte in hash)
            {
                var binaryStr = Convert.ToString(@byte, 2).PadLeft(8, '0');
                hashString.Append(binaryStr);
            }

            var leadingZeros = hashString.ToString().Split('1')[0].Length;

            return (hashString.ToString(), leadingZeros);
        }

        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
    }
}

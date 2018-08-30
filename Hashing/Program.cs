using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Hashing
{
    class Program
    {
        static SHA256Managed crypt = new SHA256Managed();
        static Random random = new Random();
        static StringBuilder hashString = new StringBuilder();
        static BigInteger maxHash = BigInteger.Pow(new BigInteger(2), 256);


        static void Main(string[] args)
        {
            var lowestHash = maxHash;
            var stopwatch = Stopwatch.StartNew();

            while (true)
            {
                var nonce = BitConverter.GetBytes(random.Next());
                var hash = crypt.ComputeHash(nonce);
                var hashInt = new BigInteger(hash.Reverse().ToArray());

                if (hashInt.Sign == 1 && hashInt < lowestHash)
                {
                    lowestHash = hashInt;

                    hashString = new StringBuilder();
                    foreach (var @byte in hash)
                    {
                        var binaryStr = Convert.ToString(@byte, 2).PadLeft(8, '0');
                        hashString.Append(binaryStr);
                    }

                    var leadingZeros = hashString.ToString().TakeWhile(c => c == '0').Count();

                    WriteLine("{0} - {1}", leadingZeros, hashInt);
                }
            }
        }
    }
}

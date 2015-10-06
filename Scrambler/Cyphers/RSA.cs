using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Cyphers
{
    public class RSA: Cypher
    {
        public int[] PublicKey;
        private int[] privateKey;
        private int p, q, n, e, d, eulerFunc;
        public Alphabets.Alphabet alphabet;

        public RSA(Alphabets.Alphabet alphabet)
        {
            this.alphabet = alphabet;
            PublicKey = new int[2];
            privateKey = new int[2];
        }

        public override string Decrypt(string text)
        {
            List<int> numbers = new List<int>();
            string[] stringNumbers = text.Split(' ');
            foreach (var stringNumber in stringNumbers)
            {
                numbers.Add(int.Parse(stringNumber));
            }

            StringBuilder result = new StringBuilder();

            foreach (var number in numbers)
            {

                Mathematics.ModularExponentiation modularExponention = new Mathematics.ModularExponentiation(number, privateKey[0], privateKey[1]);
                result.Append(alphabet[modularExponention.RaisedToThePowerModulo()]);
            }
            return result.ToString();
        }

        public override string Encrypt(string text)
        {
            List<int> letterIndexes = new List<int>();
            foreach (var letter in text)
            {
                letterIndexes.Add(alphabet.Letters.IndexOf(letter));
            }
            generateKeysTest();
            StringBuilder resultStringBuilder = new StringBuilder();

            foreach (int letterIndex in letterIndexes)
            {

                Mathematics.ModularExponentiation modularExponention = new Mathematics.ModularExponentiation(letterIndex, PublicKey[0], PublicKey[1]);
                resultStringBuilder.Append(modularExponention.RaisedToThePowerModulo() + " ");
            }

            string result = resultStringBuilder.ToString();
            result = result.Substring(0, result.Length - 1);

            return result;
        }



        private void generateKeys()
        {
            p = Mathematics.PrimeNumbers.GetPrime();
            q = Mathematics.PrimeNumbers.GetPrime();
            n = p * q;
            eulerFunc = (p - 1) * (q - 1);
            e = Mathematics.PrimeNumbers.RelativelyPrimeNumber(eulerFunc);
            Random random = new Random();
            d = 0;
            while ((e * d) % eulerFunc != 1)
                d++;
            PublicKey = new int[2] { e, n };
            privateKey = new int[2] { d, n };
        }

        private void generateKeysTest()
        {
            p = 3557;
            q = 2579;
            n = p * q;
            eulerFunc = (p - 1) * (q - 1);
            e = 3;
            Random random = new Random();
            d = 0;
            while ((e * d) % eulerFunc != 1)
                d++;
            PublicKey = new int[2] { e, n };
            privateKey = new int[2] { d, n };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Cyphers
{
    public class ElHamal : Cypher
    {
        private int[] publicKey;
        private int privateKey;
        private Alphabets.Alphabet alphabet;

        public ElHamal(Alphabets.Alphabet alphabet)
        {
            this.alphabet = alphabet;
        }

        public override string Decrypt(string text)
        {
            string[] stringNumbers = text.Split('*');
            List<int[]> cryptoGramms = new List<int[]>();
            foreach (var stringNumber in stringNumbers)
            {
                string[] numbers = stringNumber.Split(' ');
                try
                {
                    cryptoGramms.Add(new int[] { int.Parse(numbers[0]), int.Parse(numbers[1]) });
                }
                catch(FormatException e)
                { return "Format exception"; }
            }

            StringBuilder result = new StringBuilder();

            foreach (var cryptoGramm in cryptoGramms)
            {
                result.Append(alphabet[(int) (cryptoGramm[1] * Math.Pow(cryptoGramm[0], publicKey[0] - 1 - privateKey) % publicKey[0])]);
            }

            return result.ToString();
        }

        public override string Encrypt(string text)
        {
            generateKeys();
            List<int> letterIndexes = new List<int>();
            foreach (var letter in text)
            {
                letterIndexes.Add(alphabet.Letters.IndexOf(letter));
            }
            StringBuilder result = new StringBuilder();
            foreach (int index in letterIndexes)
            {
                int sessionKey = new Random().Next(2, publicKey[0] - 1); // p = publicKey[0] , g = publicKey[1], y = publicKey[2]
                double a = Math.Pow(publicKey[1], sessionKey) % publicKey[0];
                double b = Math.Pow(publicKey[2], sessionKey) * index % publicKey[0];
                result.Append(a + " " + b + "*");
            }
            
            return result.ToString().Substring(0, result.Length-1);
        }

        private void generateKeys()
        {
            int p = Mathematics.PrimeNumbers.GetPrime();
            int eulerP = p - 1;
            int g = 0;
            while (Math.Pow(g, eulerP) % p != 1)
                g++;
            int x = new Random().Next(2, p);
            int y = Mathematics.ModularExponentiation.RaisedToThePowerModulo(g, x, p);

            publicKey = new int[] { p, g, y };
            privateKey = x;
        }
    }
}

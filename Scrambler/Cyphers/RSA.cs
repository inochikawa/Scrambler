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
            p = primeNumber();
            q = primeNumber();
            n = p * q;
            eulerFunc = (p - 1) * (q - 1);
            e = relativelyPrimeNumber(eulerFunc);
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

        private int primeNumber()
        {
            int number = new Random().Next(0, 2056);
            while(!isPrime(number))
            {
                number++;
            }
            return number;
        }

        private bool isPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        private bool isRelativelyPrimeNumber(int number1, int number2)
        {
            int maxNumber = number1;
            if (number2 > number1)
                maxNumber = number2;
            for (int i = 2; i <= maxNumber; i++)
            {
                if (number1 % i == 0 && number2 % i == 0) return false;
            }
            return true;
        }

        private int relativelyPrimeNumber(int number)
        {
            Random random = new Random();
            int resultNum = 0;
            while(true)
            {
                resultNum++;
                if (!isPrime(resultNum))
                    continue;
                if (isRelativelyPrimeNumber(resultNum, number))
                    break;                
            }
            return resultNum;
        }

        private IEnumerable<int> nextPrimeNumber()
        {
            for (int i = 0; i < 10000; i++)
            {
                if (isPrime(i))
                    yield return i;
            }
        }
    }
}

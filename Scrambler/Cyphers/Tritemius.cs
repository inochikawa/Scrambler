using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Cyphers
{
    class Tritemius: Cypher
    {
        private Alphabets.Alphabet alphabet;

        private int keyA;
        private int keyB;
        private int keyC;
        public Tritemius(Alphabets.Alphabet alphabet, int a, int b, int c)
        {
            this.alphabet = alphabet;
            this.keyA = a;
            this.keyB = b;
            this.keyC = c;
        }

        public override string Encrypt(string text)
        {
            StringBuilder cryptedText = new StringBuilder();
            int indexInText = 1;
            foreach (char a in text)
            {
                cryptedText.Append(alphabetString[newSymbolIndex(a, indexInText, keyA, keyB, keyC)]);
                alphabetString.IndexOf(a);
                indexInText++;
            }
            return cryptedText.ToString();

        }
        public override string Decrypt(string text)
        {
            StringBuilder decryptedText = new StringBuilder();
            int indexInText = 1;
            foreach (char a in text)
            {
                decryptedText.Append(alphabetString[oldSymbolIndex(a, indexInText, keyA, keyB, keyC)]);
                alphabetString.IndexOf(a);
                indexInText++;
            }
            return decryptedText.ToString();
        }
        private int newSymbolIndex(char symbol, int indexInText, int a, int b, int c)
        {
            int result = (alphabetString.IndexOf(symbol) + ((int)Math.Pow(indexInText, 2) * a + (indexInText * b) + c) % alphabetString.Length);
            if (result < alphabetString.Length)
            {
                return result;
            }
            else
            {
                return (result % (alphabetString.Length - 1)) - 1;
            }

        }
        private int oldSymbolIndex(char symbol, int indexInText, int a, int b, int c)
        {
            int result = (alphabetString.IndexOf(symbol) - ((int)Math.Pow(indexInText, 2) * a + (indexInText * b) + c) % alphabetString.Length);
            if (result >= 0)
            {
                return result;
            }
            else
            {
                return alphabetString.Length - Math.Abs(result);
            }

        }

        private string alphabetString
        {
            get
            {
                StringBuilder alphabetSt = new StringBuilder();
                for (int i = alphabet.LowerCase["min"]; i <= alphabet.LowerCase["max"]; i++)
                {
                    alphabetSt.Append((char)i);
                }

                return alphabetSt.ToString();
            }
        }
    }
}

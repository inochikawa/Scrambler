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
                cryptedText.Append(alphabet.LowerCase[newSymbolIndex(a, indexInText, keyA, keyB, keyC)]);
                alphabet.LowerCase.IndexOf(a);
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
                decryptedText.Append(alphabet.LowerCase[oldSymbolIndex(a, indexInText, keyA, keyB, keyC)]);
                alphabet.LowerCase.IndexOf(a);
                indexInText++;
            }
            return decryptedText.ToString();
        }
        private int newSymbolIndex(char symbol, int indexInText, int a, int b, int c)
        {
            int result = (alphabet.LowerCase.IndexOf(symbol) + ((int)Math.Pow(indexInText, 2) * a + (indexInText * b) + c) % alphabet.LowerCase.Length);
            if (result < alphabet.LowerCase.Length)
            {
                return result;
            }
            else
            {
                return (result % (alphabet.LowerCase.Length - 1)) - 1;
            }

        }
        private int oldSymbolIndex(char symbol, int indexInText, int a, int b, int c)
        {
            int result = (alphabet.LowerCase.IndexOf(symbol) - ((int)Math.Pow(indexInText, 2) * a + (indexInText * b) + c) % alphabet.LowerCase.Length);
            if (result >= 0)
            {
                return result;
            }
            else
            {
                return alphabet.LowerCase.Length - Math.Abs(result);
            }

        }

    }
}

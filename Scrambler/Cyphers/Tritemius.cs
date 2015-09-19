using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Cyphers
{
    class Tritemius: Cypher
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public String Key { get; set; }
        private bool stringKey { get; set; }
        private Alphabets.Alphabet alphabet;


        public Tritemius(Alphabets.Alphabet _alphabet, int _a, int _b, int _c)
        {
            alphabet = _alphabet;
            A = _a;
            B = _b;
            C = _c;
            stringKey = false;
        }

        public Tritemius(Alphabets.Alphabet _alphabet, String _key)
        {
            alphabet = _alphabet;
            Key = _key;
            stringKey = true;
        }

        public override string Encrypt(String _text)
        {

            String cryptedText = "";
            int indexInText = 1;
            foreach (char a in _text)
            {
                cryptedText += alphabet.Letters[NewSymbolIndex(a, indexInText)];
                alphabet.Letters.IndexOf(a);
                indexInText++;
            }
            return cryptedText;
        }


        public override string Decrypt(String _cryptedText)
        {
            String decryptedText = "";
            int indexInText = 1;
            foreach (char a in _cryptedText)
            {
                decryptedText += alphabet.Letters[OldSymbolIndex(a, indexInText)];
                alphabet.Letters.IndexOf(a);
                indexInText++;
            }
            return decryptedText;
        }
        private int NewSymbolIndex(char symbol, int indexInText)
        {
            int result;
            if (!stringKey)
            {
                result = (alphabet.Letters.IndexOf(symbol) + ((int)Math.Pow(indexInText, 2) * A + (indexInText * B) + C)) % alphabet.Letters.Length;
            }
            else
            {
                result = (alphabet.Letters.IndexOf(symbol) + (alphabet.Letters.IndexOf(Key[(indexInText - 1) % Key.Length]) + 1)) % alphabet.Letters.Length;
            }

            return result;

        }
        private int OldSymbolIndex(char symbol, int indexInText)
        {
            int result;
            if (!stringKey)
            {
                result = (alphabet.Letters.IndexOf(symbol) - ((int)Math.Pow(indexInText, 2) * A + (indexInText * B) + C)) % alphabet.Letters.Length;
            }
            else
            {
                result = (alphabet.Letters.IndexOf(symbol) - (alphabet.Letters.IndexOf(Key[(indexInText - 1) % Key.Length]) + 1)) % alphabet.Letters.Length;
            }
            if (result >= 0)
            {
                return result;
            }
            else
            {
                return alphabet.Letters.Length - Math.Abs(result);
            }
        }
    }
}

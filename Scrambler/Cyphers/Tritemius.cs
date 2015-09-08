using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Cyphers
{
    class Tritemius: Cypher
    {
        public Alphabets.Alphabet Alphabet { get; set; }

        public int A { get; set; }

        public int B { get; set; }

        public int C { get; set; }

        public String Key { get; set; }

        private bool stringKey { get; set; }


        public Tritemius(Alphabets.Alphabet _alphabet, int _a, int _b, int _c)
        {
            this.Alphabet = _alphabet;
            this.A = _a;
            this.B = _b;
            this.C = _c;
            this.stringKey = false;
        }

        public Tritemius(Alphabets.Alphabet _alphabet, String _key)
        {
            this.Alphabet = _alphabet;
            this.Key = _key;
            this.stringKey = true;
        }

        public override string Encrypt(String _text)
        {

            String cryptedText = "";
            int indexInText = 1;
            foreach (char a in _text)
            {
                cryptedText += Alphabet.Letters[NewSymbolIndex(a, indexInText)];
                Alphabet.Letters.IndexOf(a);
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
                decryptedText += Alphabet.Letters[OldSymbolIndex(a, indexInText)];
                Alphabet.Letters.IndexOf(a);
                indexInText++;
            }
            return decryptedText;
        }
        private int NewSymbolIndex(char symbol, int indexInText)
        {
            int result;
            if (!stringKey)
            {
                result = (Alphabet.Letters.IndexOf(symbol) + ((int)Math.Pow(indexInText, 2) * A + (indexInText * B) + C)) % Alphabet.Letters.Length;
            }
            else
            {
                result = (Alphabet.Letters.IndexOf(symbol) + (Alphabet.Letters.IndexOf(Key[(indexInText - 1) % Key.Length]) + 1)) % Alphabet.Letters.Length;
            }

            return result;

        }
        private int OldSymbolIndex(char symbol, int indexInText)
        {
            int result;
            if (!stringKey)
            {
                result = (Alphabet.Letters.IndexOf(symbol) - ((int)Math.Pow(indexInText, 2) * A + (indexInText * B) + C)) % Alphabet.Letters.Length;
            }
            else
            {
                result = (Alphabet.Letters.IndexOf(symbol) - (Alphabet.Letters.IndexOf(Key[(indexInText - 1) % Key.Length]) + 1)) % Alphabet.Letters.Length;
            }
            if (result >= 0)
            {
                return result;
            }
            else
            {
                return Alphabet.Letters.Length - Math.Abs(result);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Cyphers
{
    public class XOREncryption: Cypher
    {
        string gammaKey;
        Alphabets.Alphabet alphabet;
        public XOREncryption()
        {
            
        }

        public void Init(string gammaKey, Alphabets.Alphabet alphabet)
        {
            this.alphabet = alphabet;
            this.gammaKey = gammaKey;
        }

        public override string Encrypt(string text)
        {
            int keyLenght = gammaKey.Length;
            int counter = 0;
            StringBuilder gamma = new StringBuilder();
            StringBuilder result = new StringBuilder();

            foreach (var letter in text)
            {
                if (counter == keyLenght)
                    counter = 0;
                gamma.Append(gammaKey[counter]);
                counter++;
            }

            for (int i = 0; i < text.Length; i++)
            {
                char letter = text[i];
                char gammaLetter = gamma[i];

                if (alphabet.Letters.Contains(letter))
                {
                    int indexResult = (alphabet.Letters.IndexOf(gammaLetter) + alphabet.Letters.IndexOf(letter)) % alphabet.Quantity;
                    result.Append(alphabet.Letters[indexResult]);
                }
                else
                    result.Append(letter);
            }

            return result.ToString();
        }

        public override string Decrypt(string text)
        {
            int keyLenght = gammaKey.Length;
            int counter = 0;
            StringBuilder gamma = new StringBuilder();
            StringBuilder result = new StringBuilder();

            foreach (var letter in text)
            {
                if (counter == keyLenght)
                    counter = 0;
                try
                {
                    gamma.Append(gammaKey[counter]);
                }
                catch (IndexOutOfRangeException)
                {
                    return null;
                }
                counter++;
            }

            for (int i = 0; i < text.Length; i++)
            {
                char letter = text[i];
                char gammaLetter = gamma[i];

                if (alphabet.Letters.Contains(letter))
                {
                    int indexResult = (alphabet.Letters.IndexOf(letter) - alphabet.Letters.IndexOf(gammaLetter) + alphabet.Quantity) % alphabet.Quantity;
                    result.Append(alphabet.Letters[indexResult]);
                }
                else
                    result.Append(letter);
            }

            return result.ToString();
        }
    }
}

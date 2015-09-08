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
        public XOREncryption(string gammaKey, Alphabets.Alphabet alphabet)
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
                //Если буква является большой
                if ((int)letter >= (int)alphabet.UpperCase["min"] && (int)letter <= (int)alphabet.UpperCase["max"])
                    letter = (char)((int)text[i] + 32);

                if ((int)gammaLetter >= (int)alphabet.UpperCase["min"] && (int)gammaLetter <= (int)alphabet.UpperCase["max"])
                    gammaLetter = (char)((int)gamma[i] + 32);

                //Если введена буква из алфавита, а не другой символ
                if ((int)letter >= (int)alphabet.LowerCase["min"] && (int)letter <= (int)alphabet.LowerCase["max"])
                {
                    int indexResult = ((int)gammaLetter + (int)letter - 2 * alphabet.LowerCase["min"]) % alphabet.Quantity;
                    if (indexResult == 0)
                        indexResult = alphabet.Quantity;
                    indexResult += alphabet.LowerCase["min"];
                    result.Append((char)indexResult);
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
                //Если буква является большой
                if ((int)letter >= (int)alphabet.UpperCase["min"] && (int)letter <= (int)alphabet.UpperCase["max"])
                    letter = (char)((int)text[i] - 32);

                if ((int)gammaLetter >= (int)alphabet.UpperCase["min"] && (int)gammaLetter <= (int)alphabet.UpperCase["max"])
                    gammaLetter = (char)((int)gamma[i] - 32);

                //Если введена буква из алфавита, а не другой символ
                if ((int)letter >= (int)alphabet.LowerCase["min"] && (int)letter <= (int)alphabet.LowerCase["max"])
                {
                    int indexResult = ((int)letter - (int)gammaLetter + alphabet.Quantity) % alphabet.Quantity;
                    if (indexResult == 0)
                        indexResult = alphabet.Quantity;
                    indexResult += alphabet.LowerCase["min"];
                    result.Append((char)indexResult);
                }
                else
                    result.Append(letter);
            }

            return result.ToString();
        }
    }
}

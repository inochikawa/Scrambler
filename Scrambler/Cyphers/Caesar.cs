using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Scrambler.Cyphers
{
    class Caesar: Cypher
    {
        int key;

        public Caesar(int key) :base()
        {
            this.key = key;
        }
        public Caesar()
        {

        }

        public override string Decrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            if (key > Alphabet.Quantity)
                key = key % Alphabet.Quantity;
            foreach (var letter in text)
            {
                if (Alphabet.Letters.Contains(letter))
                    if (Alphabet.Letters.IndexOf(letter) - key < 0)
                        result.Append(Alphabet.Letters[Alphabet.Letters.IndexOf(letter) - key + Alphabet.Quantity]);
                    else
                        result.Append(Alphabet.Letters[Alphabet.Letters.IndexOf(letter) - key]);
                else
                    result.Append(letter);
            }
            return result.ToString();
        }

        public override string Encrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            if (key > Alphabet.Quantity)
                key = key % Alphabet.Quantity;
            foreach (var letter in text)
            {
                if (Alphabet.Letters.Contains(letter))

                    if (Alphabet.Letters.IndexOf(letter) + key > Alphabet.Quantity - 1)
                        result.Append(Alphabet.Letters[Alphabet.Letters.IndexOf(letter) + key - Alphabet.Quantity]);
                    else
                        result.Append(Alphabet.Letters[Alphabet.Letters.IndexOf(letter) + key]);                  
                else
                    result.Append(letter);
            }
            return result.ToString();
        }
        
    }
}

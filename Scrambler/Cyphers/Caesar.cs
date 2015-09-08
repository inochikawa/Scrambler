using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Scrambler.Cyphers
{
    class Caesar:Cyphers.Cypher
    {
        int key;
        Alphabets.Alphabet alphabet;

        public Caesar(int key, Alphabets.Alphabet alphabet):base()
        {
            this.key = key;
            this.alphabet = alphabet;
        }
        public override string Decrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            if (key > alphabet.Quantity)
                key = key % alphabet.Quantity;
            foreach (var letter in text)
            {
                if (alphabet.UpperCase.Contains(letter) || alphabet.LowerCase.Contains(letter))
                    if (char.IsUpper(letter))
                    {
                        if (alphabet.UpperCase.IndexOf(letter) - key < 0)
                            result.Append(alphabet.UpperCase[alphabet.UpperCase.IndexOf(letter) - key + alphabet.Quantity]);
                        else
                            result.Append(alphabet.UpperCase[alphabet.UpperCase.IndexOf(letter) - key]);
                    }
                    else
                    {
                        if (alphabet.LowerCase.IndexOf(letter) - key < 0)
                            result.Append(alphabet.LowerCase[alphabet.LowerCase.IndexOf(letter) - key + alphabet.Quantity]);
                        else
                            result.Append(alphabet.LowerCase[alphabet.LowerCase.IndexOf(letter) - key]);
                    }
                else
                    result.Append(letter);
            }
            return result.ToString();
        }

        public override string Encrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            if (key > alphabet.Quantity)
                key = key % alphabet.Quantity;
            foreach (var letter in text)
            {
                if (alphabet.UpperCase.Contains(letter) || alphabet.LowerCase.Contains(letter))
                    if(char.IsUpper(letter))
                    {
                        if (alphabet.UpperCase.IndexOf(letter) + key > alphabet.Quantity - 1)
                            result.Append(alphabet.UpperCase[alphabet.UpperCase.IndexOf(letter) + key - alphabet.Quantity]);
                        else
                            result.Append(alphabet.UpperCase[alphabet.UpperCase.IndexOf(letter) + key]);
                    }
                    else
                    {
                        if (alphabet.LowerCase.IndexOf(letter) + key > alphabet.Quantity - 1)
                            result.Append(alphabet.LowerCase[ alphabet.LowerCase.IndexOf(letter) + key - alphabet.Quantity]);
                        else
                            result.Append(alphabet.LowerCase[alphabet.LowerCase.IndexOf(letter) + key]);
                    }
                else
                    result.Append(letter);
            }
            return result.ToString();
        }
    }
}

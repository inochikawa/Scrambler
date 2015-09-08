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
                //Если буква является строчной
                if ((int)letter >= (int)alphabet.LowerCase["min"] && (int)letter <= (int)alphabet.LowerCase["max"])
                {
                    //Если буква, после сдвига выходит за пределы алфавита
                    if ((int)letter - key > (int)alphabet.LowerCase["max"])
                        //Добавление в строку результатов символ
                        result.Append((char)((int)letter - key + alphabet.Quantity));
                    //Если буква может быть сдвинута в пределах алфавита
                    else
                        //Добавление в строку результатов символ
                        result.Append((char)((int)letter - key));
                    continue;
                }

                if (((int)letter >= (int)alphabet.UpperCase["min"]) && ((int)letter <= (int)alphabet.UpperCase["max"]))
                {
                    //Если буква, после сдвига выходит за пределы алфавита
                    if ((int)letter - key > (int)alphabet.UpperCase["max"])
                        //Добавление в строку результатов символ
                        result.Append((char)((int)letter - key + alphabet.Quantity));
                    //Если буква может быть сдвинута в пределах алфавита
                    else
                        //Добавление в строку результатов символ
                        result.Append((char)((int)letter - key));
                    continue;
                }

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
                //Если буква является строчной
                if ((int)letter >= (int)alphabet.LowerCase["min"] && (int)letter <= (int)alphabet.LowerCase["max"])
                {
                    //Если буква, после сдвига выходит за пределы алфавита
                    if ((int)letter + key > (int)alphabet.LowerCase["max"])
                        //Добавление в строку результатов символ
                        result.Append((char) ((int)letter + key - alphabet.Quantity));
                    //Если буква может быть сдвинута в пределах алфавита
                    else
                        //Добавление в строку результатов символ
                        result.Append((char) ((int)letter + key));
                    continue;
                }

                if (((int)letter >= (int)alphabet.UpperCase["min"]) && ((int)letter <= (int)alphabet.UpperCase["max"]))
                {
                    //Если буква, после сдвига выходит за пределы алфавита
                    if ((int)letter + key > (int)alphabet.UpperCase["max"])
                        //Добавление в строку результатов символ
                        result.Append((char)((int)letter + key - alphabet.Quantity));
                    //Если буква может быть сдвинута в пределах алфавита
                    else
                        //Добавление в строку результатов символ
                        result.Append((char)((int)letter + key));
                    continue;
                }

                result.Append(letter);
            }
            return result.ToString();
        }
    }
}

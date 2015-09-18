using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Cyphers
{
    public class Polinom: Cypher
    {
        Alphabets.Alphabet alphabet;
        Mathematics.Function function;
        public Polinom()
        {
            
        }
        public void Init(Alphabets.Alphabet alphabet, Mathematics.Function function)
        {
            this.alphabet = alphabet;
            this.function = function;
        }

        public override string Decrypt(string text)
        {
            List<int> letterIndexes = new List<int>();
            StringBuilder outText = new StringBuilder();
            foreach (var letter in text)
            {
                if(letter != '*' && letter != '-')
                    outText.Append(alphabet.Letters.IndexOf(letter));
                else
                    outText.Append(letter);
            }

            foreach (var word in outText.ToString().Split('*'))
            {
                letterIndexes.Add(int.Parse(word));
            }

            Mathematics.Function resultFuntion = new Mathematics.Function(letterIndexes.ToArray()) / function;

            StringBuilder result = new StringBuilder();
            foreach (var member in resultFuntion.Members)
            {
                if (member.Сoefficient < 0)
                    result.Append(alphabet.Letters[(-1 * member.Сoefficient + alphabet.Quantity) % alphabet.Quantity-1]);
                else
                    result.Append(alphabet.Letters[(member.Сoefficient + alphabet.Quantity) % alphabet.Quantity-1]);
            }

            return result.ToString();
        }

        public override string Encrypt(string text)
        {
            List<int> letterIndexes = new List<int>();
            foreach (var letter in text)
            {
                letterIndexes.Add(alphabet.Letters.IndexOf(letter)+1);
            }

            Mathematics.Function resultFuntion = new Mathematics.Function(letterIndexes.ToArray()) * function;

            StringBuilder tempResult = new StringBuilder();
            for (int i = 0; i < resultFuntion.Members.Count; i++)
            {
                tempResult.Append(resultFuntion.Members[i].Сoefficient);
                if (i != resultFuntion.Members.Count - 1)
                    tempResult.Append("*");
            }

            StringBuilder result = new StringBuilder();
            foreach (var letter in tempResult.ToString())
            {
                if (letter == '*' || letter == '-')
                    result.Append(letter);
                else
                    result.Append(alphabet[int.Parse(letter.ToString())]);
            }

            return result.ToString();
        }
    }
}

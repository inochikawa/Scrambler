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
        public Polinom(Alphabets.Alphabet alphabet, Mathematics.Function function)
        {
            this.alphabet = alphabet;
            this.function = function;
        }

        public override string Decrypt(string text)
        {
            throw new NotImplementedException();
        }

        public override string Encrypt(string text)
        {
            List<int> letterIndexes = new List<int>();
            foreach (var letter in text)
            {
                letterIndexes.Add(alphabet.Letters.IndexOf(letter));
            }

            Mathematics.Function resultFuntion = new Mathematics.Function(letterIndexes.ToArray()) * function;

            StringBuilder result = new StringBuilder();
            foreach (var index in resultFuntion.Indexes)
            {
                if (index < 0)
                    result.Append(alphabet.Letters[-1 * index % alphabet.Quantity]);
                else
                    result.Append(alphabet.Letters[index % alphabet.Quantity]);
            }

            return result.ToString();
        }
    }
}

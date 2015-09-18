using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scrambler.Cyphers
{
    public class Shtirlits: Cypher
    {
        List<string> alphabetMass = new List<string>();
        public Shtirlits()
        {
            
        }

        public void Init(Alphabets.Alphabet alphabet)
        {
            StringBuilder row = new StringBuilder();

            foreach (var letter in alphabet.Letters)
            {
                if (letter == '\n')
                {
                    alphabetMass.Add(row.ToString());
                    row.Clear();
                    continue;
                }
                row.Append(letter);
            }
        }

        public override string Encrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            List<string> results = new List<string>();
            Random random = new Random();
            int rowCounter = 0;
            int colomnCounter = 0;
            bool letterContain;

            foreach (var letter in text)
            {
                letterContain = false;
                foreach (var row in alphabetMass)
                {
                    foreach (var symbol in row)
                    {
                        if(letter == symbol)
                        {
                            results.Add(rowCounter.ToString("D2") + colomnCounter.ToString("D2") + " ");
                            letterContain = true;
                        }
                        colomnCounter++;
                    }
                    rowCounter++;
                    colomnCounter = 0;
                }
                rowCounter = 0;

                if (!letterContain) 
                    result.Append(letter);
                else
                    result.Append(results[random.Next(0, results.Count)]);

                results.Clear();
            }

            return result.ToString();
        }

        public override string Decrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            int rowCounter = 0;
            int colomnCounter = 0;
            //Convert all whitespaces to a single space.
            text = Regex.Replace(text, @"\s+", " "); 
            string[] codes = text.Split(' ');

            foreach (string code in codes)
            {
                if (code.Length != 4)
                    return "Code is invalid!";
                //'0' in char == 48 in int
                if (code[0] == '0') rowCounter = code[1]-48;
                else rowCounter = Convert.ToInt32(Convert.ToString(code[0]) + Convert.ToString(code[1]));
                if (code[2] == '0') colomnCounter = code[3]-48;
                else colomnCounter = Convert.ToInt32(Convert.ToString(code[2]) + Convert.ToString(code[3]));

                try
                {
                    result.Append(alphabetMass[rowCounter][colomnCounter]);
                }
                catch(ArgumentOutOfRangeException ex)
                {
                    return "Something wrong =(\n" + ex.Message;
                }
            }

            return result.ToString();
        }
    }
}

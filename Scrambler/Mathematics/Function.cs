using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Mathematics
{
    public class Function
    {
        public int[] Indexes;
        public Function(int[] indexes)
        {
            this.Indexes = indexes;
        }

        public static Function operator * (Function f1, Function f2) 
        {
            List<int> indexes = new List<int>();
            List<int> powers = new List<int>();
            foreach (var memberF1 in f1.split())
            {
                foreach (var memberF2 in f2.split())
                {
                    indexes.Add(f1.index(memberF1) * f2.index(memberF2));
                    powers.Add(f1.power(memberF1) + f2.power(memberF2));
                }
            }

            List<int> resultIndexes = new List<int>();
            List<int> usedPowers = new List<int>();
            int tempSum = 0;
            for (int i = 0; i < powers.Count; i++)
            {
                for (int j = 0; j < powers.Count; j++)
                {
                    if(powers[i] == powers[j] && !usedPowers.Contains(powers[i]))
                    {
                        tempSum += indexes[j];
                    }
                }
                if (!usedPowers.Contains(powers[i])) resultIndexes.Add(tempSum);
                tempSum = 0;
                usedPowers.Add(powers[i]);
            }


            return new Function(resultIndexes.ToArray());
        }

        /// <summary>
        /// split all expression on members (3x^2 for example)
        /// </summary>
        /// <returns></returns>
        private string[] split()
        {
            int indexCounter = 0;
            StringBuilder result = new StringBuilder();
            for (int i = Indexes.Length - 1; i > -1; i--)
            {
                result.Append(Indexes[indexCounter] + "x^" + i);
                if (indexCounter != Indexes.Length - 1)
                    result.Append(" ");
                indexCounter++;
            }

            return result.ToString().Split(' ');
        }

        /// <summary>
        /// power of 3x^2 is 2
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private int power(string expression)
        {
            string[] halfs = expression.Split('^');
            return Convert.ToInt32(halfs[1]);
        }

        private int index(string expression)
        {
            string[] halfs = expression.Split('x');
            return Convert.ToInt32(halfs[0]);
        }

        public override string ToString()
        {
            int indexCounter = -1;
            StringBuilder result = new StringBuilder();
            for (int i = Indexes.Length-1; i > -1; i--)
            {
                indexCounter++;
                result.Append(Indexes[indexCounter] + "x^" + i);
                if (indexCounter != Indexes.Length-1)
                    result.Append("+");
            }
            return result.ToString();
        }
    }
}

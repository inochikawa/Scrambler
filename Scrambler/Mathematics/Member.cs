using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Mathematics
{
    public class Member
    {
        public int Power;
        public int Сoefficient;
        public Member(int power, int coefficient)
        {
            this.Power = power;
            this.Сoefficient = coefficient;
        }

        public Member(string member)
        {
            if (member.Split('x').Length > 1)
            {
                string coefficientString = member.Substring(0, member.IndexOf('x'));
                if (coefficientString == "+" || coefficientString == "")
                    this.Сoefficient = 1;
                if (coefficientString == "-")
                    this.Сoefficient = -1;
                if (coefficientString[0] == '+') coefficientString.Remove(0, 1);
                this.Сoefficient = int.Parse(coefficientString);

                if (member.IndexOf('^') > -1)
                    this.Power = int.Parse(member.Substring(member.IndexOf('^') + 1, member.Length - member.IndexOf('^') - 1));
                else
                    this.Power = 1;
            }
            else
            {
                try
                {
                    this.Сoefficient = int.Parse(member);
                }
                catch (FormatException)
                {
                    this.Сoefficient = 0;
                }
                this.Power = 0;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (this.Power != 0)
            {
                if (this.Сoefficient == 1)
                    result.Append("+x");
                else if (this.Сoefficient == -1)
                    result.Append("-x");
                else if (this.Сoefficient == 0)
                    return "0";
                else if(this.Сoefficient > 1)
                    result.Append("+" + this.Сoefficient.ToString() + "x");
                else
                    result.Append(this.Сoefficient.ToString() + "x");
            }
            else
                return this.Сoefficient.ToString();

            if (this.Power == 1)
                return result.ToString();
            else
                result.Append("^" + this.Power);

            return result.ToString();
        }
    }
}

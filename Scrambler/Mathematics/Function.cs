using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Mathematics
{
    public class Function
    {
        public List<Member> Members = new List<Member>();
        public Function(string expression)
        {
            if (validateExpression(expression))
            {
                Members = new List<Member>();
                StringBuilder member = new StringBuilder();
                for (int i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == '+' || expression[i] == '-')
                    {
                        Members.Add(new Member(member.ToString()));
                        member.Clear();
                    }
                    member.Append(expression[i]);
                }
                Members.Add(new Member(member.ToString()));
            }
            else
            {
                new Function(new int[0]);
            }
        }

        public Function(Member[] members)
        {
            foreach (var member in members)
                this.Members.Add(member);
        }

        public Function(int[] coefficients)
        {
            this.Members = new List<Member>();
            int powerCounter = coefficients.Length - 1;
            foreach (var coefficient in coefficients)
            {
                this.Members.Add(new Member(powerCounter, coefficient));
                powerCounter--;
            }
        }

        private bool validateExpression(string expression)
        {
            if (expression.Length == 0)
                return false;

            expression = expression.Trim();
            expression = expression.Replace(" ", "");
            while (expression.IndexOf("--") > -1 | expression.IndexOf("++") > -1 | expression.IndexOf("^^") > -1 | expression.IndexOf("xx") > -1)
            {
                expression = expression.Replace("--", "-");
                expression = expression.Replace("++", "+");
                expression = expression.Replace("^^", "^");
                expression = expression.Replace("xx", "x");
            }
            string ValidChars = "+-x1234567890^";
            bool result = true;
            foreach (char c in expression)
            {
                if (ValidChars.IndexOf(c) == -1)
                    result = false;
            }
            return result;
        }

        public static Function operator * (Function f1, Function f2) 
        {
            List<Member> members = new List<Member>();
            foreach (var memberF1 in f1.Members)
                foreach (var memberF2 in f2.Members)
                    members.Add(new Member(memberF1.Power + memberF2.Power, 
                                           memberF1.Сoefficient * memberF2.Сoefficient));

            return new Function(bringSimilarMembers(members));
        }

        private static Member[] bringSimilarMembers(List<Member> members)
        {
            List<int> usedPowers = new List<int>();
            List<Member> resultMembers = new List<Member>();
            int tempSum = 0;
            for (int i = 0; i < members.Count; i++)
            {
                for (int j = 0; j < members.Count; j++)
                {
                    if (members[i].Power == members[j].Power && !usedPowers.Contains(members[i].Power))
                    {
                        tempSum += members[j].Сoefficient;
                    }
                }
                if (!usedPowers.Contains(members[i].Power))
                    resultMembers.Add(new Member(members[i].Power, tempSum));
                tempSum = 0;
                usedPowers.Add(members[i].Power);
            }
            return resultMembers.ToArray();
        }

        public void Sort()
        {
            List<Member> resultMembers = new List<Member>();
            int membersCount = Members.Count;
            while (membersCount > 0)
            {
                Member maxMember = Members[0];
                foreach (Member m in Members)
                {
                    if (m.Power > maxMember.Power)
                    {
                        maxMember = m;
                    }
                }
                resultMembers.Add(maxMember);
                Members.Remove(maxMember);
                membersCount--;
            }

            foreach (Member m in resultMembers)
            {
                Members.Add(m);
            }
        }

        private void refreshMembers()
        {
            List<Member> resultMembers = new List<Member>();
            foreach (var member in Members)
            {
                if (member.Сoefficient != 0)
                    resultMembers.Add(member);
            }

            Members.Clear();
            Members.AddRange(resultMembers);
        }

        public static Function operator /(Function f1, Function f2)
        {
            f1.Sort();
            f2.Sort();
            List<Member> resultMembers = new List<Member>();
            if (f1.Members[0].Power < f2.Members[0].Power)
                throw new Exception("Invalid Division: P1.MaxPower is Lower than P2.MaxPower");
            while (f1.Members[0].Power >= f2.Members[0].Power && f1.Members[0].Сoefficient >= f2.Members[0].Сoefficient)
            {
                Member NextResult = new Member(f1.Members[0].Power - f2.Members[0].Power, f1.Members[0].Сoefficient / f2.Members[0].Сoefficient);
                resultMembers.Add(NextResult);
                Function TempPoly = NextResult;

                Function NewPoly = TempPoly * f2;
                f1 = f1 - NewPoly;
                f1.refreshMembers();
                if (f1.Members.Count < 1)
                    break;
            }
            return new Function(resultMembers.ToArray());
        }

        /// <summary>
        /// Implicit Conversion : this will Convert the single Term to the Poly. 
        /// First it Creates a new Instance of TermCollection and Add The Term to it. 
        /// Second Creates a new Poly by the TermCollection and Return it.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static implicit operator Function(Member t)
        {
            List<Member> members = new List<Member>();
            members.Add(t);
            return new Function(members.ToArray());
        }

        /// <summary>
        /// Minus Operations: Like Plus Operation but at first we just Make the Second Poly to the Negetive Value.
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns></returns>
        public static Function operator -(Function f1, Function f2)
        {
            foreach (Member t in f2.Members)
                t.Сoefficient *= -1;

            return f1 + f2;
        }

        /// <summary>
        /// Plus Operator: 
        /// Add Method of TermsCollection will Check the Power of each Term And if it's already 
        /// exists in the Collection Just Plus the Coefficient of the Term and This Mean Plus Operation.
        /// So We Simply Add the Terms of Second Poly to the First one.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Function operator +(Function p1, Function p2)
        {
            List<Member> resultMembers = new List<Member>();
            resultMembers.AddRange(p1.Members);
            resultMembers.AddRange(p2.Members);
            
            return new Function(bringSimilarMembers(resultMembers));
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Members.Count; i++) 
                result.Append(Members[i]);  
            if (result[0] == '+')
                result.Remove(0, 1);

            return result.ToString();
        }
    }
}

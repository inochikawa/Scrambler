using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Polynomial.Net
{
    public class Poly
    {
        #region Constructor Overloading:
        /// <summary>
        /// Constructor which Read String and find Terms in it. Create new Term for each
        /// Term String and add it to the Terms Collection. 
        /// </summary>
        /// <param name="PolyExpression"></param>
        public Poly(string PolyExpression)
        {
            this._Terms = new TermCollection();
            this.ReadPolyExpression(PolyExpression);
        }

        /// <summary>
        /// Constructor which create a new instance of Poly with a predefined TermCollection.
        /// </summary>
        /// <param name="terms"></param>
        public Poly(TermCollection terms)
        {
            this.Terms = terms;
            this.Terms.Sort(TermCollection.SortType.ASC);
        }

        #endregion

        #region Destructor:
        /// <summary>
        /// Clear the Term Collections
        /// </summary>
        ~Poly()
        {
            this.Terms.Clear();
        }

        #endregion 

        #region Override methods:

        /// <summary>
        /// This will Print out the string Format of Polynomial. by Calling each Term in the collection.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            this.Terms.Sort(TermCollection.SortType.DES);

            string result = string.Empty;
            foreach (Term t in this.Terms)
            {
                result += t.ToString();
            }
            if (result.Substring(0, 1) == "+")
                result = result.Remove(0, 1);
            return result;
        }

        #endregion

        #region Methods:
        /// <summary>
        /// Calculate the Value of Polynomial with the given X value.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public long Calculate(int x)
        {
            long result = 0;
            foreach (Term t in this.Terms)
            {
                result += (long)t.Coefficient * (long)(Math.Pow(x, t.Power));
            }
            return result;
        }

        /// <summary>
        /// Static method which Validate the input Expression
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool ValidateExpression(string Expression)
        {
            if (Expression.Length == 0)
                return false;

            Expression = Expression.Trim();
            Expression = Expression.Replace(" ", "");
            while (Expression.IndexOf("--") > -1 | Expression.IndexOf("++") > -1 | Expression.IndexOf("^^") > -1 | Expression.IndexOf("xx") > -1)
            {
                Expression = Expression.Replace("--", "-");
                Expression = Expression.Replace("++", "+");
                Expression = Expression.Replace("^^", "^");
                Expression = Expression.Replace("xx", "x");
            }
            string ValidChars = "+-x1234567890^";
            bool result = true;
            foreach (char c in Expression)
            {
                if (ValidChars.IndexOf(c) == -1)
                    result = false;
            }
            return result;
        }

        /// <summary>
        /// Read Method will Identify any Term in the Expression and Create a new Instance of 
        /// Term Class and add it to the TermCollection
        /// </summary>
        /// <param name="PolyExpression">input string of Polynomial Expression</param>
        private void ReadPolyExpression(string PolyExpression)
        {
            if(ValidateExpression(PolyExpression))
            {
                string NextChar = string.Empty;
                string NextTerm = string.Empty;
                for (int i = 0 ; i < PolyExpression.Length; i++)
                {
                    NextChar = PolyExpression.Substring(i, 1);
                    if ((NextChar == "-" | NextChar == "+") & i > 0)
                    {
                        Term TermItem = new Term(NextTerm);
                        this.Terms.Add(TermItem);
                        NextTerm = string.Empty;
                    }
                    NextTerm += NextChar;
                }
                Term Item = new Term(NextTerm);
                this.Terms.Add(Item);
                
                this.Terms.Sort(TermCollection.SortType.ASC);
            }
            else
            {
                throw new Exception("Invalid Polynomial Expression");
            }
        }

        #endregion

        #region Fields & Properties:

        /// <summary>
        /// Terms Property, Type of TermCollection
        /// </summary>
        private TermCollection _Terms;
        public TermCollection Terms
        {
            get
            {
                return _Terms;
            }
            set
            {
                _Terms = value;
            }
        }


        /// <summary>
        /// Read-Only Property return the Length of TermCollection which means length of Polynomial Expression.
        /// </summary>
        public int Lentgh
        {
            get
            {
                return this.Terms.Length;
            }
        }
        #endregion

        #region Operator OverLoading:

        /// <summary>
        /// Plus Operator: 
        /// Add Method of TermsCollection will Check the Power of each Term And if it's already 
        /// exists in the Collection Just Plus the Coefficient of the Term and This Mean Plus Operation.
        /// So We Simply Add the Terms of Second Poly to the First one.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Poly operator +(Poly p1, Poly p2)
        {
            Poly result = new Poly(p1.ToString());
            foreach (Term t in p2.Terms)
                result.Terms.Add(t);
            return result;
        }

        /// <summary>
        /// Minus Operations: Like Plus Operation but at first we just Make the Second Poly to the Negetive Value.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Poly operator -(Poly p1, Poly p2)
        {
            Poly result = new Poly(p1.ToString());
            Poly NegetiveP2 = new Poly(p2.ToString());
            foreach (Term t in NegetiveP2.Terms)
                t.Coefficient *= -1;

            return result + NegetiveP2;
        }
       
        /// <summary>
        /// Multiple Operation: For each term in the First Poly We Multiple it in the Each Term of Second Poly
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Poly operator *(Poly p1, Poly p2)
        {
            TermCollection result = new TermCollection();
            int counter = 0;
            foreach (Term t1 in p1.Terms)
            {
                foreach (Term t2 in p2.Terms)
                {
                    result.Add(new Term(t1.Power + t2.Power,t1.Coefficient * t2.Coefficient));
                    counter++;
                }
            }
            return new Poly(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Poly operator /(Poly p1, Poly p2)
        {
            p1.Terms.Sort(TermCollection.SortType.DES);
            p2.Terms.Sort(TermCollection.SortType.DES);
            TermCollection resultTerms = new TermCollection();
            if (p1.Terms[0].Power < p2.Terms[0].Power)
                throw new Exception("Invalid Division: P1.MaxPower is Lower than P2.MaxPower");
            while(p1.Terms[0].Power > p2.Terms[0].Power)
            {
                Term NextResult = new Term(p1.Terms[0].Power - p2.Terms[0].Power, p1.Terms[0].Coefficient / p2.Terms[0].Coefficient);
                resultTerms.Add(NextResult);
                Poly TempPoly = NextResult;

                Poly NewPoly = TempPoly * p2;
                p1 = p1 - NewPoly;
            }
            return new Poly(resultTerms);
        }

        /// <summary>
        /// this will Create a new Poly by the Value of 1 and Plus it to the First Poly.
        /// </summary>
        /// <param name="p1"></param>
        /// <returns></returns>
        public static Poly operator ++(Poly p1)
        {
            Poly p2 = new Poly("1");
            p1 = p1 + p2;
            return p1;
        }

        /// <summary>
        /// this will Create a new Poly by the Value of -1 and Plus it to the First Poly.
        /// </summary>
        /// <param name="p1"></param>
        /// <returns></returns>
        public static Poly operator --(Poly p1)
        {
            Poly p2 = new Poly("-1");
            p1 = p1 + p2;
            return p1;
        }

        /// <summary>
        /// Implicit Conversion : this will Convert the single Term to the Poly. 
        /// First it Creates a new Instance of TermCollection and Add The Term to it. 
        /// Second Creates a new Poly by the TermCollection and Return it.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static implicit operator Poly(Term t)
        {
            TermCollection Terms = new TermCollection();
            Terms.Add(t);
            return new Poly(Terms);
        }

        /// <summary>
        /// Implicit Conversion: this will Create new Instance of Poly by the String Constructor
        /// And return it.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static implicit operator Poly(string expression)
        {
            return new Poly(expression);
        }

        /// <summary>
        /// Implicit Conversion: this will Create new Instance of Poly by the String Constructor
        /// And return it.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Poly(int value)
        {
            return new Poly(value.ToString());
        }
        #endregion
    }
}

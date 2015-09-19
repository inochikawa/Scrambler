using System;
using System.Collections.Generic;
using System.Text;

namespace Polynomial.Net
{
    public class Term
    {

        #region Constructors:

        /// <summary>
        /// Simple Constructor which Create a new Instanse of Term Class
        /// With 2 parameters
        ///  
        /// </summary>
        /// <param name="power"></param>
        /// <param name="coefficient"></param>
        public Term(int power,int coefficient)
        {
            this.Power = power;
            this.Coefficient = coefficient;
        }

        /// <summary>
        /// Constructor Overload which Create a new Instance of Term Class
        /// With a simple string and try to read the Power and Coefficient
        /// by identifing the input string
        /// </summary>
        /// <param name="TermExpression"></param>
        public Term(string TermExpression)
        {
            if (TermExpression.Length > 0)
            {
                if (TermExpression.IndexOf("x^") > -1)
                {
                    string CoefficientString = TermExpression.Substring(0, TermExpression.IndexOf("x^"));
                    int IndexofX = TermExpression.IndexOf("x^");
                    string PowerString = TermExpression.Substring(IndexofX + 2, (TermExpression.Length -1) - (IndexofX + 1));
                    if (CoefficientString == "-")
                        this.Coefficient = -1;
                    else if (CoefficientString == "+" | CoefficientString == "")
                        this.Coefficient = 1;
                    else
                        this.Coefficient = int.Parse(CoefficientString);
                    
                    this.Power = int.Parse(PowerString);
                }
                else if (TermExpression.IndexOf("x") > -1)
                {
                    this.Power = 1;
                    string CoefficientString = TermExpression.Substring(0, TermExpression.IndexOf("x"));
                    if (CoefficientString == "-")
                        this.Coefficient = -1;
                    else if (CoefficientString == "+" | CoefficientString == "")
                        this.Coefficient = 1;
                    else
                        this.Coefficient = int.Parse(CoefficientString);
                }
                else
                {
                    this.Power = 0;
                    this.Coefficient = int.Parse(TermExpression);
                }
            }
            else
            {
                this.Power = 0;
                this.Coefficient = 0;
            }
        }
        #endregion

        #region Override Methods:
        /// <summary>
        /// This Override will push the Term in a String Form to the output.
        /// ToString() method will write the String Format of the Term Like: 3x^2
        /// Which means [Coefficient]x^[Power].
        /// This Method Also check if it's needed to have x^,x,- or + in the pattern.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string Result = string.Empty;
            if (Coefficient != 0)
            {
                if (this.Coefficient > 0)
                    Result += "+";
                else
                    Result += "-";

                if (this.Power == 0)
                    Result += (this.Coefficient < 0 ? this.Coefficient * -1 : this.Coefficient).ToString();
                else if (this.Power == 1)
                    if (this.Coefficient > 1 | this.Coefficient < -1)
                        Result += string.Format("{0}x",(this.Coefficient <0 ? this.Coefficient * -1 : this.Coefficient).ToString());
                    else
                        Result += "x";
                else
                    if (this.Coefficient > 1 | this.Coefficient < -1)
                        Result += string.Format("{0}x^{1}", (this.Coefficient < 0 ? this.Coefficient * -1 : this.Coefficient).ToString(), this.Power.ToString());
                    else
                        Result += string.Format("x^{0}",this.Power.ToString());
            }
            return Result;
        }

        #endregion

        #region Fields & Properties:
        /// <summary>
        /// Private field to hold the Power Value.
        /// </summary>
        private int _Power;
        
        /// <summary>
        /// Private field to hold the Cofficient Value.
        /// </summary>
        private int _coefficient;

        /// <summary>
        /// Power Property
        /// Notice: Set Method Check if the value is Negetive and Make it Positive.
        /// </summary>
        public int Power
        {
            get
            {
                return _Power;
            }
            set
            {
                if (value < 0)
                    _Power = value * -1;
                else
                    _Power = value;
                
            }
        }

        /// <summary>
        /// Coefficient Property
        /// </summary>
        public int Coefficient
        {
            get
            {
                return _coefficient;
            }
            set
            {
                _coefficient = value;
            }
        }
        #endregion
    }
}

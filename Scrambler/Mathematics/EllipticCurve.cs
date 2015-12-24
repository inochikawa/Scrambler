using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Mathematics
{
    /// <summary>
    /// y^2 = x^3 + ax + b
    /// </summary>
    public class EllipticCurve
    {
        private int a, b;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="funtion">Example: x^3 + ax + b</param>
        public EllipticCurve(Function funtion)
        {
            if (funtion.Members.Count > 3) return;
            a = funtion.Members[1].Сoefficient;
            b = funtion.Members[2].Сoefficient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression">Example: "y^2 = x^3 + ax + b"</param>
        public EllipticCurve(string expression)
        {
            string[] expressinons = expression.Split(new char[] {' ', '=', ' ' });
            Function fcn = new Function(expressinons[expressinons.Length - 1]);
            if (fcn.Members.Count > 3) return;
            a = fcn.Members[1].Сoefficient;
            b = fcn.Members[2].Сoefficient;
        }

        public EllipticCurve(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        public EllipticCurve()
        {

        }

        public void PrimeRandomEllipticCurve()
        {
            a = Mathematics.PrimeNumbers.GetPrime();
            b = Mathematics.PrimeNumbers.GetPrime();
        }

        public bool IsPointOnEllipticCurve(int x, int y)
        {
            if (y * y == Math.Pow(x, 3) + a * x + b)
                return true;
            else
                return false;
        }

        public int[] RandomPoint()
        {
            int[] point = new int[2];
            int x = new Random().Next(-100, 100);
            int y = 0;
            while (!IsPointOnEllipticCurve(x, y))
                y++;
            point[0] = x;
            point[1] = y;

            return point;
        }

        public override string ToString()
        {
            return "y^2 = x^3 + " + a + "x + " + b;
         }
    }
}

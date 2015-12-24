using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Mathematics
{
    public class ECPoint
    {
        public BigInteger x;
        public BigInteger y;
        public BigInteger a;
        public BigInteger b;
        public BigInteger FieldChar;

        public ECPoint(ECPoint p)
        {
            x = p.x;
            y = p.y;
            a = p.a;
            b = p.b;
            FieldChar = p.FieldChar;
        }

        public ECPoint()
        {
            x = new BigInteger();
            y = new BigInteger();
            a = new BigInteger();
            b = new BigInteger();
            FieldChar = new BigInteger();
        }

        public ECPoint(BigInteger x, BigInteger y)
        {
            this.x = x;
            this.y = y;
            a = new BigInteger();
            b = new BigInteger();
            FieldChar = new BigInteger();
        }

        //сложение двух точек P1 и P2
        public static ECPoint operator +(ECPoint leftPoint, ECPoint rightPoint)
        {
            ECPoint resultPoint = new ECPoint();
            resultPoint.a = leftPoint.a;
            resultPoint.b = leftPoint.b;
            resultPoint.FieldChar = leftPoint.FieldChar;

            BigInteger dy = rightPoint.y - leftPoint.y;
            BigInteger dx = rightPoint.x - leftPoint.x;

            if (dx < 0)
                dx += leftPoint.FieldChar;
            if (dy < 0)
                dy += leftPoint.FieldChar;

            BigInteger m = (dy * modInverse(dx, leftPoint.FieldChar)) % leftPoint.FieldChar;
            if (m < 0)
                m += leftPoint.FieldChar;
            resultPoint.x = (m * m - leftPoint.x - rightPoint.x) % leftPoint.FieldChar;
            resultPoint.y = (m * (leftPoint.x - resultPoint.x) - leftPoint.y) % leftPoint.FieldChar;
            if (resultPoint.x < 0)
                resultPoint.x += leftPoint.FieldChar;
            if (resultPoint.y < 0)
                resultPoint.y += leftPoint.FieldChar;
            return resultPoint;
        }

        //сложение точки P c собой же
        public static ECPoint Double(ECPoint p)
        {
            ECPoint p2 = new ECPoint();
            p2.a = p.a;
            p2.b = p.b;
            p2.FieldChar = p.FieldChar;

            BigInteger dy = 3 * p.x * p.x + p.a;
            BigInteger dx = 2 * p.y;

            if (dx < 0)
                dx += p.FieldChar;
            if (dy < 0)
                dy += p.FieldChar;

            BigInteger m = (dy * modInverse(dx, p.FieldChar)) % p.FieldChar;
            p2.x = (m * m - p.x - p.x) % p.FieldChar;
            p2.y = (m * (p.x - p2.x) - p.y) % p.FieldChar;
            if (p2.x < 0)
                p2.x += p.FieldChar;
            if (p2.y < 0)
                p2.y += p.FieldChar;

            return p2;
        }

        //умножение точки на число x, по сути своей представляет x сложений точки самой с собой
        public static ECPoint Multiply(BigInteger x, ECPoint p)
        {
            ECPoint temp = p;
            x = x - 1;
            while (x != 0)
            {

                if ((x % 2) != 0)
                {
                    if ((temp.x == p.x) || (temp.y == p.y))
                        temp = Double(temp);
                    else
                        temp = temp + p;
                    x = x - 1;
                }
                x = x / 2;
                p = Double(p);
            }
            return temp;
        }

        private static BigInteger modInverse(BigInteger a, BigInteger b)
        {
            BigInteger b0 = b, t, q;
            BigInteger x0 = 0, x1 = 1;
            if (b == 1) return 1;
            while (a > 1)
            {
                q = a / b;
                t = b;
                b = a % b;
                a = t;
                t = x0;
                x0 = x1 - q * x0;
                x1 = t;
            }
            if (x1 < 0) x1 += b0;
            return x1;
        }
    }
}

using Scrambler.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Cyphers
{
    public class CryptosystemBasedOnEllipticCurves : Cypher
    {
        private BigInteger p = new BigInteger();
        private BigInteger a = new BigInteger();
        private BigInteger b = new BigInteger();
        private BigInteger n = new BigInteger();
        private byte[] xG;
        private ECPoint G = new ECPoint();
        private string signature;

        public ECPoint Q { get; private set; }

        public CryptosystemBasedOnEllipticCurves()
        {

        }

        public CryptosystemBasedOnEllipticCurves(BigInteger p, BigInteger a, BigInteger b, BigInteger n, byte[] xG)
        {
            this.a = a;
            this.b = b;
            this.n = n;
            this.p = p;
            this.xG = xG;
        }

        //Генерируем секретный ключ заданной длины
        public BigInteger GenPrivateKey(int BitSize)
        {
            BigInteger d = new BigInteger();
            
            
                d.genRandomBits(BitSize, new Random());
            
            return d;
        }

        //С помощью секретного ключа d вычисляем точку Q=d*G, это и будет наш публичный ключ
        public ECPoint GenPublicKey(BigInteger d)
        {
            ECPoint G = GDecompression();
            ECPoint Q = ECPoint.multiply(d, G);
            return Q;
        }

        //Восстанавливаем координату y из координаты x и бита четности y 
        private ECPoint GDecompression()
        {
            byte y = xG[0];
            byte[] x = new byte[xG.Length - 1];
            Array.Copy(xG, 1, x, 0, xG.Length - 1);
            BigInteger Xcord = new BigInteger(x);
            BigInteger temp = (Xcord * Xcord * Xcord + a * Xcord + b) % p;
            BigInteger beta = ModSqrt(temp, p);
            BigInteger Ycord = new BigInteger();
            if ((beta % 2) == (y % 2))
                Ycord = beta;
            else
                Ycord = p - beta;
            ECPoint G = new ECPoint();
            G.a = a;
            G.b = b;
            G.FieldChar = p;
            G.x = Xcord;
            G.y = Ycord;
            this.G = G;
            return G;
        }

        //функция вычисления квадратоного корня по модулю простого числа q
        public BigInteger ModSqrt(BigInteger a, BigInteger q)
        {
            BigInteger b = new BigInteger();
            do
            {
                b.genRandomBits(255, new Random());
            } while (Legendre(b, q) == 1);
            BigInteger s = 0;
            BigInteger t = q - 1;
            while ((t & 1) != 1)
            {
                s++;
                t = t >> 1;
            }
            BigInteger InvA = a.modInverse(q);
            BigInteger c = b.modPow(t, q);
            BigInteger r = a.modPow(((t + 1) / 2), q);
            BigInteger d = new BigInteger();
            for (int i = 1; i < s; i++)
            {
                BigInteger temp = 2;
                temp = temp.modPow((s - i - 1), q);
                d = (r.modPow(2, q) * InvA).modPow(temp, q);
                if (d == (q - 1))
                    r = (r * c) % q;
                c = c.modPow(2, q);
            }
            return r;
        }

        //Вычисляем символ Лежандра
        public BigInteger Legendre(BigInteger a, BigInteger q)
        {
            return a.modPow((q - 1) / 2, q);
        }

        //подписываем сообщение
        public string GenerateSignature(byte[] h, BigInteger d)
        {
            BigInteger alpha = new BigInteger(h);
            BigInteger e = alpha % n;
            if (e == 0)
                e = 1;
            BigInteger k = new BigInteger();
            ECPoint C = new ECPoint();
            BigInteger r = new BigInteger();
            BigInteger s = new BigInteger();
            do
            {
                do
                {
                    k.genRandomBits(n.bitCount(), new Random());
                } while ((k < 0) || (k > n));
                C = ECPoint.multiply(k, G);
                r = C.x % n;
                s = ((r * d) + (k * e)) % n;
            } while ((r == 0) || (s == 0));
            string Rvector = padding(r.ToHexString(), n.bitCount() / 4);
            string Svector = padding(s.ToHexString(), n.bitCount() / 4);
            return Rvector + Svector;
        }

        //проверяем подпись 
        public bool CheckSignature(byte[] H, string sing, ECPoint Q)
        {
            string Rvector = sing.Substring(0, n.bitCount() / 4);
            string Svector = sing.Substring(n.bitCount() / 4, n.bitCount() / 4);
            BigInteger r = new BigInteger(Rvector, 16);
            BigInteger s = new BigInteger(Svector, 16);
            if ((r < 1) || (r > (n - 1)) || (s < 1) || (s > (n - 1)))
                return false;
            BigInteger alpha = new BigInteger(H);
            BigInteger e = alpha % n;
            if (e == 0)
                e = 1;
            BigInteger v = e.modInverse(n);
            BigInteger z1 = (s * v) % n;
            BigInteger z2 = n + ((-(r * v)) % n);
            this.G = GDecompression();
            ECPoint A = ECPoint.multiply(z1, G);
            ECPoint B = ECPoint.multiply(z2, Q);
            ECPoint C = A + B;
            BigInteger R = C.x % n;
            if (R == r)
                return true;
            else
                return false;
        }

        //дополняем подпись нулями слева до длины n, где n - длина модуля в битах
        private string padding(string input, int size)
        {
            if (input.Length < size)
            {
                do
                {
                    input = "0" + input;
                } while (input.Length < size);
            }
            return input;
        }

        private byte[] FromHexStringToByte(string input)
        {
            byte[] data = new byte[input.Length / 2];
            string HexByte = "";
            for (int i = 0; i < data.Length; i++)
            {
                HexByte = input.Substring(i * 2, 2);
                data[i] = Convert.ToByte(HexByte, 16);
            }
            return data;
        }

        public override string Decrypt(string text)
        {
            GOST hash = new GOST(256);
            byte[] H = hash.GetHash(Encoding.Default.GetBytes(text));
            return CheckSignature(H, signature, Q).ToString();
        }

        public override string Encrypt(string text)
        {
            p = new BigInteger("6277101735386680763835789423207666416083908700390324961279", 10);
            a = new BigInteger("-3", 10);
            b = new BigInteger("64210519e59c80e70fa7e9ab72243049feb8deecc146b9b1", 16);
            xG = FromHexStringToByte("03188da80eb03090f67cbf20eb43a18800f4ff0afd82ff1012");
            n = new BigInteger("ffffffffffffffffffffffff99def836146bc9b1b4d22831", 16);
            BigInteger d = GenPrivateKey(192);
            Q = GenPublicKey(d);
            GOST hash = new GOST(256);
            byte[] H = hash.GetHash(Encoding.Default.GetBytes(text));
            signature = GenerateSignature(H, d);
            return signature;
        }
    }
}

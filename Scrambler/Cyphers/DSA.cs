using System;
using Scrambler.Mathematics;

namespace Scrambler.Cyphers
{
    public class DSA : Cypher
    {
        private int q, p, g, r, s, y, x;
        private bool isCalcSignature;

        public DSA()
        {
            q = PrimeNumbers.GetPrime();
            p = 10;
            q = numberExpansion(q);
            while((p-1) % q != 0)
                p++;

            while (g != Math.Pow(2, (p-1)/ q) % p)
                g++;
            
            x = privateKey();
            y = publicKey();

            isCalcSignature = false;
        }

        private int numberExpansion(int number)
        {
            int numberLenth = heshSum("test").ToBits().Length;
            int[] numberBits = number.ToBits();
            int[] result = new int[numberLenth];
            int differenceLenth = numberLenth - numberBits.Length;
            for (int i = differenceLenth; i < numberLenth; i++)
            {
                result[i] = numberBits[i - differenceLenth];
            }

            return Convert.ToInt32(result.ToStringArray(), 2);
        }

        public string SignatureMessage(string text)
        {
            r = 0; s = 0;
            while (r == 0 || s == 0)
            {
                int k = new Random().Next(0, q);
                r = ModularExponentiation.RaisedToThePowerModulo(g, k, p) % p;

                s = (int)Math.Pow(k, -1) * (heshSum(text) + r * x) % q;
            }
            isCalcSignature = true;
            return r + " " + s;
        }

        private int heshSum(string data="Test")
        {
            SHA1 sha1 = new SHA1();
            return (int) Convert.ToUInt32(sha1.HeshSum(data), 16);
        }

        public bool CheckSignature(string text)
        {
            if (!isCalcSignature)
                return false;

            double w = Math.Pow(s, -1) % q;
            double u1 = (heshSum(text) * w) % q;
            double u2 = (r * w) % q;
            int v = (int) (Math.Pow(g, u1) * Math.Pow(y, u2) % p) % q;
            if (v != r)
                return true;
            else
                return false;  
        }

        private int publicKey()
        {
            return (int) Math.Pow(g, x) % p;
        }

        private int privateKey()
        {
            return new Random().Next(0, q);
        }

        public override string Decrypt(string text)
        {
            return CheckSignature(text).ToString();
        }

        public override string Encrypt(string text)
        {
            return SignatureMessage(text);
        }
    }
}

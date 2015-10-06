using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Cyphers
{
    public class DiffiHellmana : Cypher
    {
        private int privateKey, publicKey, basis, moduler;

        public DiffiHellmana()
        {

        }

        public override string Decrypt(string text)
        {
            return algorithmExample();
        }

        public override string Encrypt(string text)
        {
            return algorithmExample();
        }

        private string algorithmExample()
        {
            basis = Mathematics.PrimeNumbers.GetPrime();
            moduler = Mathematics.PrimeNumbers.GetPrime();

            int a = Mathematics.PrimeNumbers.GetPrime();
            Mathematics.ModularExponentiation meA = new Mathematics.ModularExponentiation(basis, a, moduler);
            int A = meA.RaisedToThePowerModulo();


            int b = Mathematics.PrimeNumbers.GetPrime();
            Mathematics.ModularExponentiation meB = new Mathematics.ModularExponentiation(basis, b, moduler);
            int B = meB.RaisedToThePowerModulo();

            int keyA = Mathematics.ModularExponentiation.RaisedToThePowerModulo(B, a, moduler);
            int keyB = Mathematics.ModularExponentiation.RaisedToThePowerModulo(A, b, moduler);

            return  "1) Generated two prime numbers: p = " + basis.ToString() + ", g = " + moduler.ToString() + "\n" +
                    "2) Alice choosed number a = " + a.ToString() + " and sent to Bob number A = " + A.ToString() + "\n" +
                    "3) Bob choosed number b = " + a.ToString() + " and sent to Alice number B = " + B.ToString() + "\n" +
                    "4) Alice generate her key = " + keyA.ToString() + "\n" +
                    "5) Bob generate his key = " + keyB.ToString();
        }
    }
}

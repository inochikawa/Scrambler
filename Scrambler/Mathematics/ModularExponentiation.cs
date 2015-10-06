using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Mathematics
{
    public class ModularExponentiation
    {
        private int basis, power, module;

        public ModularExponentiation(int basis, int power, int module)
        {
            this.module = module;
            this.basis = basis;
            this.power = power;
        }

        public int RaisedToThePowerModulo()
        {
            int[] powerBits = Mathematics.Converter.GetBitsFromInt(power);
            List<BigInteger> resultIteration = new List<BigInteger>() { basis };

            for (int i = 0; i < powerBits.Length; i++)
            {
                if(i + 1 != powerBits.Length)
                    if (powerBits[i + 1] == 0)
                        resultIteration.Add(BigInteger.Pow(resultIteration[i], 2) % module);
                    else
                        resultIteration.Add((BigInteger.Pow(resultIteration[i], 2) * basis) % module);
            }

            return (int) resultIteration.Last();
        }

        public static int RaisedToThePowerModulo(int basis, int power, int module)
        {
            int[] powerBits = Mathematics.Converter.GetBitsFromInt(power);
            List<BigInteger> resultIteration = new List<BigInteger>() { basis };

            for (int i = 0; i < powerBits.Length; i++)
            {
                if (i + 1 != powerBits.Length)
                    if (powerBits[i + 1] == 0)
                        resultIteration.Add(BigInteger.Pow(resultIteration[i], 2) % module);
                    else
                        resultIteration.Add((BigInteger.Pow(resultIteration[i], 2) * basis) % module);
            }

            return (int)resultIteration.Last();
        }
    }
}

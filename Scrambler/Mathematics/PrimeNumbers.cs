using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Mathematics
{
    public static class PrimeNumbers
    {
        public static int GetPrime()
        {
            int number = new Random().Next(0, 2056);
            while (!IsPrime(number))
            {
                number++;
            }
            System.Threading.Thread.Sleep(50);
            return number;
        }

        public static bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        public static bool IsRelativelyPrimeNumber(int number1, int number2)
        {
            int maxNumber = number1;
            if (number2 > number1)
                maxNumber = number2;
            for (int i = 2; i <= maxNumber; i++)
            {
                if (number1 % i == 0 && number2 % i == 0) return false;
            }
            return true;
        }

        public static int RelativelyPrimeNumber(int number)
        {
            Random random = new Random();
            int resultNum = 0;
            while (true)
            {
                resultNum++;
                if (!IsPrime(resultNum))
                    continue;
                if (IsRelativelyPrimeNumber(resultNum, number))
                    break;
            }
            System.Threading.Thread.Sleep(50);
            return resultNum;
        }
    }
}

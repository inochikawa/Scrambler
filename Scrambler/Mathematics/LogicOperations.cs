using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Mathematics
{
    public static class LogicOperations
    {
        public static int AND(int x, int y)
        {
            if (x + y == 2)
                return 1;
            else
                return 0;
        }

        public static int NOT(int x)
        {
            if (x == 1)
                return 0;
            else if (x == 0)
                return 1;

            return 0;
        }

        public static int OR(int x, int y)
        {
            if (x + y == 0)
                return 0;
            else
                return 1;
        }

        public static int XOR(int x, int y)
        {
            if ((x + y) % 2 == 0)
                return 0;
            else
                return 1;
        }
    }
}

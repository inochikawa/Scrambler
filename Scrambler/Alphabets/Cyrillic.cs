using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Alphabets
{
    public class Cyrillic: Alphabet
    {
        public Cyrillic()
        {
            LowerCase.Add("min", 1072);
            LowerCase.Add("max", 1103);
            UpperCase.Add("min", 1040);
            UpperCase.Add("max", 1071);

            Quantity = 33;
        }
    }
}

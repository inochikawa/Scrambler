using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Alphabets
{
    public class Roman: Alphabet
    {        
        public Roman()
        {
            LowerCase.Add("min", 97);
            LowerCase.Add("max", 122);
            UpperCase.Add("min", 65);
            UpperCase.Add("max", 90);
            Quantity = 23;
        }
    }
}

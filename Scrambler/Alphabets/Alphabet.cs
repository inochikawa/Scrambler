using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Alphabets
{
    public abstract class Alphabet
    {
        public int Quantity { get; set; }
        public Dictionary<string, int> LowerCase;
        public Dictionary<string, int> UpperCase;

        public Alphabet()
        {
            LowerCase = new Dictionary<string, int>();
            UpperCase = new Dictionary<string, int>();
        }
    }
}

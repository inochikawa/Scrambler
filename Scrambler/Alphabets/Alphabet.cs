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
        public string UpperCase { get; set; }
        public string LowerCase { get; set; }

        public Alphabet()
        {
            
        }
    }
}

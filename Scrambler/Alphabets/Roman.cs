using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Alphabets
{
    public class Roman: Alphabet
    {        
        public Roman(string a, string b):base(a,b)
        {
            Letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ,.";
            Quantity = Letters.Length;
        }
    }
}

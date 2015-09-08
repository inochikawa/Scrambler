using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.ViewModel
{
    public class AlphabetsWindowVM
    {
        public AlphabetsWindowVM()
        {

        }

        public List<Alphabets.Alphabet> alphabets
        {
            get
            {
                Alphabets.Alphabet.Load();
                return Alphabets.Alphabet.Alphabets;
            }
        }
    }
}

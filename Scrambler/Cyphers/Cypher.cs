using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrambler.Cyphers
{
    public abstract class Cypher
    {
        public Cypher()
        {
        }

        public abstract string Encrypt(string text);

        public abstract string Decrypt(string text);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Scrambler.Strategies
{
    [Attributes.Strategy(Type = typeof(Cyphers.MD4))]
    class MD5St : Strategy
    {
        public MD5St()
        {
            createNewCypher();
        }
        public override void AddElements(StackPanel parent)
        {
            return;
        }

        public override string Decrypt(string text)
        {
            return Cypher.Decrypt(text);
        }

        public override void DeleteElements(StackPanel parent)
        {
            return;
        }

        public override string Encrypt(string text)
        {
            return Cypher.Encrypt(text);
        }

        protected override void createNewCypher()
        {
            Cypher = new Cyphers.MD4();
        }
    }
}

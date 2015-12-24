using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Scrambler.Strategies
{
    [Attributes.Strategy(Type = typeof(Cyphers.SHA1))]
    class SHA1St : Strategy
    {
        public SHA1St()
        {
            createNewCypher();
        }
        public override void AddElements(StackPanel parent)
        {
            return;
        }

        public override string Decrypt(string text)
        {
            createNewCypher();
            return Cypher.Decrypt(text);
        }

        public override void DeleteElements(StackPanel parent)
        {
            return;
        }

        public override string Encrypt(string text)
        {
            createNewCypher();
            return Cypher.Encrypt(text);
        }

        protected override void createNewCypher()
        {
            Cypher = new Cyphers.SHA1();
        }
    }
}

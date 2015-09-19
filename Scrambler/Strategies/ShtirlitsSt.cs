using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Strategies
{
    [Attributes.Strategy(Type=typeof(Cyphers.Shtirlits))]
    public class ShtirlitsSt: Strategy
    {
        
        public ShtirlitsSt():base()
        {

        }

        public override void AddElements(System.Windows.Controls.StackPanel parent)
        {
            return;
        }

        public override void DeleteElements(System.Windows.Controls.StackPanel parent)
        {
            return;
        }

        public override string Encrypt(string text)
        {
            createNewCypher();
            return Cypher.Encrypt(text);
        }

        public override string Decrypt(string text)
        {
            createNewCypher();
            return Cypher.Decrypt(text);
        }

        protected override void createNewCypher()
        {
            Cypher = new Cyphers.Shtirlits(Alphabet);
        }
    }
}

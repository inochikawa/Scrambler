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
        Cyphers.Shtirlits shtirlits; 
        public ShtirlitsSt():base()
        {
            try
            {
                shtirlits = new Cyphers.Shtirlits();
            }
            catch (FormatException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Key is invalid!");
            }
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
            shtirlits.Init(Cypher.Alphabet);
            return shtirlits.Encrypt(text);
        }

        public override string Decrypt(string text)
        {
            shtirlits.Init(Cypher.Alphabet);
            return shtirlits.Decrypt(text);
        }

        protected override void createNewCypher()
        {
            throw new NotImplementedException();
        }
    }
}

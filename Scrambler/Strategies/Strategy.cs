using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Scrambler.Strategies
{
    public abstract class Strategy
    {
        protected Alphabets.Alphabet alphabet;
        private InputLanguage inputLanguage { get; set; }
        public abstract void AddElements(StackPanel parent);
        public abstract void DeleteElements(StackPanel parent);
        public abstract string Encrypt(string text);
        public abstract string Decrypt(string text);

        public Strategy()
        {
            inputLanguage = InputLanguage.CurrentInputLanguage;
            if (inputLanguage.Culture.EnglishName == "Russian (Russia)")
                alphabet = new Alphabets.Cyrillic();
            else
                alphabet = new Alphabets.Roman();
        }
    }
}

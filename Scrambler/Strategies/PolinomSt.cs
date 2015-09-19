using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Scrambler.Strategies
{
    [Attributes.Strategy(Type=typeof(Cyphers.Polinom))]
    public class PolinomSt:Strategy
    {
        Grid grid;
        TextBox txtKey;
        Label label;
        Cyphers.Polinom polinom;

        public PolinomSt()
        {
            try
            {
                polinom = new Cyphers.Polinom();
            }
            catch (FormatException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Key is invalid!");
            }

            grid = new Grid();
            txtKey = new TextBox();
            txtKey.Name = "txtKey";
            txtKey.Width = 100;
            txtKey.Height = 23;
            txtKey.Text = "";
            txtKey.HorizontalAlignment = HorizontalAlignment.Right;

            label = new Label();
            label.Name = "label";
            label.Content = "Polinom indexes:";
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;

            grid.Children.Add(txtKey);
            grid.Children.Add(label);
        }

        public override void AddElements(StackPanel parent)
        {
            parent.Children.Add(grid);
        }

        public override string Encrypt(string text)
        {
            polinom.Init(Cypher.Alphabet, new Mathematics.Function(txtKey.Text));
            return polinom.Encrypt(text);
        }

        public override string Decrypt(string text)
        {
            polinom.Init(Cypher.Alphabet, new Mathematics.Function(txtKey.Text));
            return polinom.Decrypt(text);
        }

        public override void DeleteElements(StackPanel parent)
        {
            parent.Children.Remove(grid);
        }

        private int[] indexes(string text)
        {
            List<int> ind = new List<int>();
            string[] splitText = text.Split(' ');

            foreach (var member in splitText)
            {
                ind.Add(Convert.ToInt32(member));
            }

            return ind.ToArray();
        }

        protected override void createNewCypher()
        {
            throw new NotImplementedException();
        }
    }
}

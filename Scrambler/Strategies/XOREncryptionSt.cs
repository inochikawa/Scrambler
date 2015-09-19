using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Scrambler.Strategies
{
    [Attributes.Strategy(Type=typeof(Cyphers.XOREncryption))]
    public class XOREncryptionSt: Strategy
    {
        Grid grid;
        TextBox txtKey;
        Label label;

        public XOREncryptionSt():base()
        {
            grid = new Grid();
            txtKey = new TextBox();
            txtKey.Name = "txtKey";
            txtKey.Width = 100;
            txtKey.Height = 23;
            txtKey.Text = "";
            txtKey.HorizontalAlignment = HorizontalAlignment.Right;

            label = new Label();
            label.Name = "label";
            label.Content = "Gamma:";
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
            createNewCypher();
            return Cypher.Encrypt(text);
        }

        public override string Decrypt(string text)
        {
            createNewCypher();
            return Cypher.Decrypt(text);
        }

        public override void DeleteElements(StackPanel parent)
        {
            parent.Children.Remove(grid);
        }

        protected override void createNewCypher()
        {
            Cypher = new Cyphers.XOREncryption(txtKey.Text, Alphabet);
        }
    }
}

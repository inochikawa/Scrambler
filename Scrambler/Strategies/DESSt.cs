using System;
using System.Windows;
using System.Windows.Controls;

namespace Scrambler.Strategies
{
    [Attributes.Strategy(Type =typeof(Cyphers.DES))]
    public class DESSt : Strategy
    {
        Grid grid;
        TextBox txtKey;
        Label label;

        public DESSt()
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
            label.Content = "Enter the key:";
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;

            grid.Children.Add(txtKey);
            grid.Children.Add(label);
        }
        public override void AddElements(StackPanel parent)
        {
            parent.Children.Add(grid);
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

        public override string Encrypt(string text)
        {
            createNewCypher();
            return Cypher.Encrypt(text);
        }

        protected override void createNewCypher()
        {
            try
            {
                Cypher = new Cyphers.DES(txtKey.Text);
            }
            catch (FormatException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Key is invalid!");
            }
        }
    }
}

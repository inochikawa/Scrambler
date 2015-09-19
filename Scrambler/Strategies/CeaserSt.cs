using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Scrambler.Strategies
{
    [Attributes.Strategy(Type=typeof(Cyphers.Caesar))]
    public class CeaserSt: Strategies.Strategy
    {
        Grid grid;
        TextBox txtKey;
        Label label;
        string previousKey;

        public CeaserSt():base()
        {
            grid = new Grid();
            txtKey = new TextBox();
            txtKey.Name = "txtKey";
            txtKey.Width = 100;
            txtKey.Height = 23;
            txtKey.Text = "";
            txtKey.HorizontalAlignment = HorizontalAlignment.Right;
            txtKey.LostFocus += TxtKey_LostFocus;
            txtKey.GotFocus += TxtKey_GotFocus;

            label = new Label();
            label.Name = "label";
            label.Content = "Enter the key:";
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;

            grid.Children.Add(txtKey);
            grid.Children.Add(label);
        }

        private void TxtKey_GotFocus(object sender, RoutedEventArgs e)
        {
            previousKey = txtKey.Text;
        }

        private void TxtKey_LostFocus(object sender, RoutedEventArgs e)
        {
            if (previousKey.Equals(txtKey.Text))
                return;
            else
            {
                KeyChange = true;
            }
        }

        protected override void createNewCypher()
        {
            try
            {
                Cypher = new Cyphers.Caesar(Convert.ToInt32(txtKey.Text), Alphabet);
            }
            catch (FormatException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Key is invalid!");
            }
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
    }
}

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
        Cyphers.Caesar ceaser;

        public CeaserSt():base()
        {
            try
            {
                ceaser = new Cyphers.Caesar();
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

        public override string Encrypt(string text)
        {
            return ceaser.Encrypt(text);
        }

        public override string Decrypt(string text)
        {
            ceaser.Init(Convert.ToInt32(txtKey.Text), Alphabet);
            return ceaser.Decrypt(text);
        }

        public override void DeleteElements(StackPanel parent)
        {
            parent.Children.Remove(grid);
        }
    }
}

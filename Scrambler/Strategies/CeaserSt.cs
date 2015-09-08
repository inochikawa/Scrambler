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

        public CeaserSt():base()
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

        public override string Encrypt(string text)
        {
            string result;
            Cyphers.Caesar ceaser;
            try
            {                
                ceaser = new Cyphers.Caesar(Convert.ToInt32(txtKey.Text), alphabet);
            }
            catch (FormatException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Key is invalid!");
                return null;
            }
            result = ceaser.Encrypt(text);

            return result;
        }

        public override string Decrypt(string text)
        {
            string result;
            Cyphers.Caesar ceaser;
            try
            {
                ceaser = new Cyphers.Caesar(Convert.ToInt32(txtKey.Text), alphabet);
            }
            catch (FormatException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Key is invalid!");
                return null;
            }
            result = ceaser.Decrypt(text);

            return result;
        }

        public override void DeleteElements(StackPanel parent)
        {
            parent.Children.Remove(grid);
        }
    }
}

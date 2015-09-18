using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Scrambler.Strategies

{
    [Attributes.Strategy(Type=typeof(Cyphers.Tritemius))]
    public class TritemiusSt: Strategy
    {
        Grid grid;
        List<TextBox> textBoxes;
        Cyphers.Tritemius tritemius;

        public TritemiusSt()
        {
            grid = new Grid();
            textBoxes = new List<TextBox>();

            try
            {
                tritemius = new Cyphers.Tritemius();
            }
            catch (FormatException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Key is invalid!");
            }
        }

        public override void AddElements(System.Windows.Controls.StackPanel parent)
        {
            double margin = 0.0;
            int labelNum = 1;

            for (int i = 0; i < 3; i++)
            {
                Label label = new Label();
                label.Name = "label" + labelNum.ToString();
                label.Content = "Key " + labelNum + ":";
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.VerticalAlignment = VerticalAlignment.Top;
                label.Margin = new Thickness(0.0, margin, 0.0, 0.0);


                TextBox txtKey = new TextBox();
                txtKey.Name = "txtKey" + labelNum.ToString();
                txtKey.Width = 100;
                txtKey.Height = 23;
                txtKey.Text = "";
                txtKey.HorizontalAlignment = HorizontalAlignment.Right;
                txtKey.VerticalAlignment = VerticalAlignment.Top;
                txtKey.Margin = new Thickness(0.0, margin, 0.0, 0.0);
                textBoxes.Add(txtKey);

                margin += 30.0;
                labelNum++;
                grid.Children.Add(label);
                grid.Children.Add(txtKey);
            }

            parent.Children.Add(grid);
        }

        public override string Decrypt(string text)
        {
            return tritemius.Decrypt(text);
        }

        public override void DeleteElements(System.Windows.Controls.StackPanel parent)
        {
            parent.Children.Remove(grid);
        }

        public override string Encrypt(string text)
        {
            tritemius.Init(Alphabet, Convert.ToInt32(textBoxes[0].Text), Convert.ToInt32(textBoxes[1].Text), Convert.ToInt32(textBoxes[2].Text));
            return tritemius.Encrypt(text);
        }
    }
}

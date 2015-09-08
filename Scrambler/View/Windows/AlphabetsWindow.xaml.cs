using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Scrambler.View.Windows
{
    /// <summary>
    /// Interaction logic for AlphabetsWindow.xaml
    /// </summary>
    public partial class AlphabetsWindow : Window
    {
        public AlphabetsWindow()
        {
            InitializeComponent();
        }

        private void SaveAllButton_Click(object sender, RoutedEventArgs e)
        {
            Alphabets.Alphabet.Save();
        }

        private void RemoveItButton_Click(object sender, RoutedEventArgs e)
        {
            Alphabets.Alphabet.Alphabets.Remove((Alphabets.Alphabet)alphabetsListBox.SelectedItem);
            refresh();
        }

        private void AddAlphabetButton_Click(object sender, RoutedEventArgs e)
        {
            new Alphabets.Alphabet("Unnamed","abcdefghijklmnopqrstuvwxyz");
            refresh();
        }

        private void EditItButton_Click(object sender, RoutedEventArgs e)
        {
            ((Alphabets.Alphabet)alphabetsListBox.SelectedItem).Letters = alphabetLettersTextBox.Text;
            ((Alphabets.Alphabet)alphabetsListBox.SelectedItem).Name = alphabetNameTextBox.Text;
            refresh();
        }

        private void alphabetsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            alphabetLettersTextBox.Text = ((Alphabets.Alphabet)alphabetsListBox.SelectedItem).Letters;
            alphabetNameTextBox.Text = ((Alphabets.Alphabet)alphabetsListBox.SelectedItem).Name;
        }

        private void refresh()
        {
            alphabetsListBox.Items.Refresh();
        }

        private void ChooseItButton_Click(object sender, RoutedEventArgs e)
        {
            ((Alphabets.Alphabet)alphabetsListBox.SelectedItem).Chosen = true;
            foreach (Alphabets.Alphabet item in alphabetsListBox.Items)
                if ((Alphabets.Alphabet)alphabetsListBox.SelectedItem != item)
                    item.Chosen = false;
        }
    }
}

using Scrambler.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scrambler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, Strategies.Strategy> strategyDictionary = new Dictionary<string, Strategies.Strategy>();
        private Strategies.Strategy selectedStrategy;

        public MainWindow()
        {
            InitializeComponent();
            loadCyphers();

            Alphabets.Alphabet.Load();
            ChosenAlphabetLabel.Content = "Chosen alphabet: " + chosenAlphabet();
        }

        private void btnCodify(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedStrategy.Cypher.Alphabet = chosenAlphabet();
                txtOut.Text = selectedStrategy.Encrypt(txtIn.Text);
            }
            catch (NullReferenceException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Cypher was not selected!");
                return;
            }
        }
        
        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedStrategy.Cypher.Alphabet = chosenAlphabet();
                txtOut.Text = selectedStrategy.Decrypt(txtIn.Text);
            }
            catch (NullReferenceException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Cypher was not selected!");
                return;
            }
        }

        private void loadCyphers()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Cyphers.Caesar));
            Type[] types = assembly.DefinedTypes.ToArray();
            foreach (var type in types)
            {
                if (Attribute.IsDefined(type, typeof(Attributes.Strategy)))
                {
                    var attrValue = Attribute.GetCustomAttribute(type, typeof(Attributes.Strategy)) as Attributes.Strategy;
                    if(attrValue.Type != null)
                    {
                        cmbCypher.Items.Add(attrValue.Type.Name);
                        strategyDictionary.Add(attrValue.Type.Name, (Strategies.Strategy)Activator.CreateInstance(type));
                    }
                }
            }
        }

        private void cmbCypher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            onSelectedStrategy();
        }
        
        private void txtIn_TextChanged(object sender, TextChangedEventArgs e)
        {
            cmbCypher.IsEnabled = true;

            if (txtIn.Text == "") cmbCypher.IsEnabled = false; 
        }

        private void AlphabetsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new AlphabetsWindow().ShowDialog();
            ChosenAlphabetLabel.Content = "Chosen alphabet: " + chosenAlphabet();
            onSelectedStrategy();
        }

        private void onSelectedStrategy()
        {
            if (selectedStrategy != null) selectedStrategy.DeleteElements(stackPanel);

            selectedStrategy = strategyDictionary[(string)cmbCypher.SelectedItem];
            selectedStrategy.AddElements(stackPanel);
            selectedStrategy.Cypher.Alphabet = chosenAlphabet();
        }

        private Alphabets.Alphabet chosenAlphabet()
        {
            foreach (Alphabets.Alphabet item in Alphabets.Alphabet.Alphabets)
                if (item.Chosen)
                    return item;
            return null;
        }
    }
}

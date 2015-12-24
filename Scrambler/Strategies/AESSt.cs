using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Scrambler.Strategies
{
    [Attributes.Strategy(Type = typeof(Cyphers.AES))]
    public class AESSt:Strategy
    {
        Grid grid;
        TextBox txtKey;
        Label label;
        Label labelKeySize;

        public AESSt()
        {
            grid = new Grid();
            txtKey = new TextBox();
            txtKey.Name = "txtKey";
            txtKey.Width = 100;
            txtKey.Height = 23;
            txtKey.Text = "";
            txtKey.HorizontalAlignment = HorizontalAlignment.Right;
            txtKey.TextChanged += TxtKey_TextChanged;
            txtKey.BorderThickness = new Thickness(2.0);

            label = new Label();
            label.Name = "label";
            label.Content = "Enter the key:";
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;

            labelKeySize = new Label();
            labelKeySize.Name = "labelKeySize";
            labelKeySize.Content = "Key size: ";
            labelKeySize.HorizontalAlignment = HorizontalAlignment.Left;
            labelKeySize.VerticalAlignment = VerticalAlignment.Top;

            grid.Children.Add(txtKey);
            grid.Children.Add(label);
        }

        private void TxtKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            labelKeySize.Content = "Key size: " + Mathematics.Converter.ToBits(txtKey.Text).Length;
            if (Mathematics.Converter.ToBits(txtKey.Text).Length != 64)
                txtKey.Foreground = Brushes.Red;
            else
                txtKey.Foreground = Brushes.Green;
        }

        public override void AddElements(StackPanel parent)
        {
            parent.Children.Add(grid);
            parent.Children.Add(labelKeySize);
        }

        public override string Decrypt(string text)
        {
            //createNewCypher();
            //return Cypher.Decrypt(text);

            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(text);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(txtKey.Text);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }

        public override void DeleteElements(StackPanel parent)
        {
            parent.Children.Remove(grid);
            parent.Children.Remove(labelKeySize);
        }

        public override string Encrypt(string text)
        {
            //createNewCypher();
            //return Cypher.Encrypt(text);

            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(text);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(txtKey.Text);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        protected override void createNewCypher()
        {
            try
            {
                Cypher = new Cyphers.AES();
            }
            catch (FormatException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Key is invalid!");
            }
        }

        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
    }
}

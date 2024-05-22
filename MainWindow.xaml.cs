using PasswordCracker.Models;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordCracker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordTextBox.Text;
            if (!string.IsNullOrEmpty(password))
            {
                string encryptedPassword = PasswordEncryption.EncryptPassword(password);
                ResultTextBox.Text = encryptedPassword;
            }
            else
            {
                StatusTextBlock.Text = "Please enter a password.";
            }
        }

        private async void BruteForceButton_Click(object sender, RoutedEventArgs e)
        {
            string encryptedPassword = ResultTextBox.Text;
            if (!string.IsNullOrEmpty(encryptedPassword))
            {
                ProgressBar.Value = 0;
                StatusTextBlock.Text = "Starting brute force attack...";
                var progress = new Progress<int>(value => ProgressBar.Value = value);
                var result = await BruteForce.CrackPasswordAsync(encryptedPassword, 5, progress);
                StatusTextBlock.Text = result.Item1 != null ? $"Password found: {result.Item1}" : "Password not found.";
                TimeTextBlock.Text = $"Time taken: {result.Item2} ms";
            }
            else
            {
                StatusTextBlock.Text = "Please encrypt a password first.";
            }
        }
    }
}

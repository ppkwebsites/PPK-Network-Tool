using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace PPKT
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PlaceholderText.Visibility = Visibility.Visible; // Show placeholder initially
        }

        private void IpconfigButton_Click(object sender, RoutedEventArgs e)
        {
            outputTextBox.Clear();
            RunCommand("ipconfig");
        }

        private void ArpButton_Click(object sender, RoutedEventArgs e)
        {
            outputTextBox.Clear();
            RunCommand("arp -a");
        }

        private void PingButton_Click(object sender, RoutedEventArgs e)
        {
            outputTextBox.Clear();
            string ipAddress = CommandTextBox.Text.Trim();

            if (!string.IsNullOrWhiteSpace(ipAddress))
            {
                RunCommand($"ping {ipAddress}");
            }
            else
            {
                outputTextBox.Text = "Please enter a valid IP address or hostname.";
            }
        }

        private void TracertButton_Click(object sender, RoutedEventArgs e)
        {
            outputTextBox.Clear();
            string ipAddress = CommandTextBox.Text.Trim();

            if (!string.IsNullOrWhiteSpace(ipAddress))
            {
                RunCommand($"tracert {ipAddress}");
            }
            else
            {
                outputTextBox.Text = "Please enter a valid IP address or hostname.";
            }
        }

        private void CurlButton_Click(object sender, RoutedEventArgs e)
        {
            outputTextBox.Clear();
            RunCommand("curl ifconfig.me");
        }

        private void CommandTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Manage visibility of the placeholder
            PlaceholderText.Visibility = string.IsNullOrWhiteSpace(CommandTextBox.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void RunCommand(string command)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c " + command,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            outputTextBox.Text = output + Environment.NewLine;
            process.WaitForExit();
        }
    }
}

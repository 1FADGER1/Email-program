using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Win32;
using System.IO;

namespace Sender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> attachmentsPath = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = attachmentsPath.ToArray();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string filePath;
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                filePath = openFileDialog.FileName;
                attachmentsPath.Add(filePath);
                listBox.ItemsSource = attachmentsPath;
            }
            
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if(listBox.SelectedItem != null)
            {
                attachmentsPath.Remove((string)listBox.SelectedItem);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            EmailService emailService = new EmailService();
            emailService.SendingEmail(textBox.Text, attachmentsPath);
        }
    }
}

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
using System.ComponentModel;
using DBCurse.Models;

namespace DBCurse
{
    /// <summary>
    /// Логика взаимодействия для AddAgreement.xaml
    /// </summary>
    public partial class AddAgreement : Window
    {
        public AddAgreement()
        {
            InitializeComponent();
            Loaded += AddAgreement_Loaded;
        }

        private void AddAgreement_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Client c in DataModel.Clients)
            {
                comboBox.Items.Add(c.Name);
            }
            comboBox.SelectedItem = (DataContext as Agreement).ClientName;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AddClient f = new AddClient();
            f.DataContext = new Client();
            f.Owner = this;
            f.ShowDialog();
            if (f.DataContext != null && f.DialogResult == true)
            {
                DataModel.Clients.Add(f.DataContext as Client);
            }
        }
    }
}

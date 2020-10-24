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
using DBCurse.Views;

namespace DBCurse
{
    /// <summary>
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        public Clients()
        {
            InitializeComponent();
            Loaded += Clients_Loaded;
        }

        private void Clients_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new ClientsViewModel();
        }

        private void Button_AddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClient f = new AddClient();
            f.DataContext = new Client();
            f.Owner = this;
            f.ShowDialog();
            if (f.DataContext != null && f.DialogResult == true)
            {
                if ((f.DataContext as Client).Telephone.ToString().Length != 6 || (f.DataContext as Client).Telephone.ToString().Length != 11)
                {
                    System.Windows.Forms.MessageBox.Show("Неверно введен номер телефона.");
                }
                else
                DataModel.Clients.Add(f.DataContext as Client);
            }
        }
        private void Button_DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            DataModel.Clients.Remove(dataGrid.SelectedItem as Client);
        }
        private void Button_EditClient_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                AddClient f = new AddClient();
                f.DataContext = (dataGrid.SelectedItem as Client);
                f.Owner = this;
                f.ShowDialog();
            }
            else System.Windows.Forms.MessageBox.Show("Выберите клиента для редактирования.");
        }
    }
}

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
    /// Логика взаимодействия для Operations.xaml
    /// </summary>
    public partial class Operations : Window
    {
        public Operations()
        {
            InitializeComponent();
            Loaded += Operations_Loaded;
        }

        private void Operations_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new OperationsViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddOperation f = new AddOperation();
            f.DataContext = new Operation();
            f.Owner = this;
            f.ShowDialog();
            if (f.DataContext != null && f.DialogResult == true)
            {
                DataModel.Operations.Add(f.DataContext as Operation);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataModel.Operations.Remove(dataGrid.SelectedItem as Operation);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                AddOperation f = new AddOperation();
                f.DataContext = (dataGrid.SelectedItem as Operation);
                f.Owner = this;
                f.ShowDialog();
            }
            else System.Windows.Forms.MessageBox.Show("Выберите операцию для редактирования.");
        }
    }
}

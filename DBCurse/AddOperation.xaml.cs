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
    /// Логика взаимодействия для AddOperation.xaml
    /// </summary>
    public partial class AddOperation : Window
    {
        public AddOperation()
        {
            InitializeComponent();
            Loaded += AddOperation_Loaded;
        }

        private void AddOperation_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Operation c in DataModel.Operations)
            {
                comboBox.Items.Add(c.OperationName);
            }
            comboBox.SelectedItem = (this.DataContext as Operation).OperationName;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}

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
using DBCurse.Models;

namespace DBCurse
{
    /// <summary>
    /// Логика взаимодействия для AddDepartment.xaml
    /// </summary>
    public partial class AddDepartment : Window
    {
        public AddDepartment()
        {
            InitializeComponent();
            Loaded += AddDepartment_Loaded;
        }

        private void AddDepartment_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Worker w in DataModel.Workers)
            {
                comboBox_LeaderName.Items.Add(w.Surname);
            }
            comboBox_LeaderName.SelectedItem = (this.DataContext as Department).LeaderName;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}

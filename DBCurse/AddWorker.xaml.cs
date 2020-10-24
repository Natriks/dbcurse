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
    /// Логика взаимодействия для AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Window
    {
        public AddWorker()
        {
            InitializeComponent();
            Loaded += AddWorker_Loaded;
        }
        private void AddWorker_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Position p in DataModel.Positions)
            {
                comboBox_Position.Items.Add(p.Name);
            }
            comboBox_Position.SelectedItem = (DataContext as Worker).PositionName;
            for (int i = 1; i<11;i++)
                comboBox_Performance.Items.Add(i);
            comboBox_Performance.SelectedItem = (DataContext as Worker).Performance;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}

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
using DBCurse.ProductiveModel;

namespace DBCurse
{
    /// <summary>
    /// Логика взаимодействия для AddProductiveRule.xaml
    /// </summary>
    public partial class AddProductiveRule : Window
    {
        public AddProductiveRule()
        {
            InitializeComponent();
            Loaded += AddProductiveRule_Loaded;
        }
        List<string> listValues;
        List<string> listPrior;
        private void AddProductiveRule_Loaded(object sender, RoutedEventArgs e)
        {
            listValues = new List<string>();
            listValues.Add("Низкая");
            listValues.Add("Средняя");
            listValues.Add("Высокая");
            listValues.Add("Очень высокая");
            foreach (string value in listValues)
            {
                comboBox_ProjectType.Items.Add(value);
                comboBox_ProjectType_Copy.Items.Add(value);
                comboBox_ProjectType_Copy1.Items.Add(value);
                comboBox_ProjectType_Copy2.Items.Add(value);
            }

            listPrior = new List<string>();
            listPrior.Add("Очень высокий");
            listPrior.Add("Высокий");
            listPrior.Add("Средний");
            listPrior.Add("Низкий");
            listPrior.Add("Очень Низкий");
            foreach (string value in listPrior)
            {
                comboBox_ProjectType_Copy3.Items.Add(value);
            }
            comboBox_ProjectType.SelectedItem = (DataContext as Productive).ProjectValue;
            comboBox_ProjectType_Copy.SelectedItem = (DataContext as Productive).ProjectUrgency;
            comboBox_ProjectType_Copy1.SelectedItem = (DataContext as Productive).WorkerBusy;
            comboBox_ProjectType_Copy2.SelectedItem = (DataContext as Productive).WorkerPerformance;
            comboBox_ProjectType_Copy3.SelectedItem = (DataContext as Productive).Priority;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}

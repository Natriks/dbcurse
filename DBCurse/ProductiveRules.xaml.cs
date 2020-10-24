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
    /// Логика взаимодействия для ProductiveRules.xaml
    /// </summary>
    public partial class ProductiveRules : Window
    {
        public ProductiveRules()
        {
            InitializeComponent();
        }
        private void Button_AddRule_Click(object sender, RoutedEventArgs e)
        {
            AddProductiveRule f = new AddProductiveRule();
            f.DataContext = new Productive();
            f.Owner = this;
            f.ShowDialog();
            if (f.DataContext != null && f.DialogResult == true)
                DataModel.Productives.Add(f.DataContext as Productive);
        }
        private void Button_DeleteRule_Click(object sender, RoutedEventArgs e)
        {
            DataModel.Productives.Remove(dataGrid.SelectedItem as Productive);
        }
        private void Button_EditRule_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                AddProductiveRule f = new AddProductiveRule();
                f.DataContext = (dataGrid.SelectedItem as Productive);
                f.Owner = this;
                f.ShowDialog();
            }
            else System.Windows.Forms.MessageBox.Show("Выберите правило для редактирования.");
        }
        private void Button_EditValue_Click_1(object sender, RoutedEventArgs e)
        {
            EditProdRuleAttributes f = new EditProdRuleAttributes();
            f.Owner = this;
            f.ShowDialog();
        }
    }
}

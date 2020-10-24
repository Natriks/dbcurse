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
    /// Логика взаимодействия для Investors.xaml
    /// </summary>
    public partial class Investors : Window
    {
        public Investors()
        {
            InitializeComponent();
            Loaded += Investors_Loaded;
        }

        private void Investors_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new InvestorsViewModel();
        }

        private void Button_AddInvestor_Click(object sender, RoutedEventArgs e)
        {
            AddInvestor f = new AddInvestor();
            f.DataContext = new Investor();
            f.Owner = this;
            f.ShowDialog();
            if (f.DialogResult == true && (f.DataContext as Investor).Description == null)
                (f.DataContext as Investor).Description = "Отсутствует.";
            if (f.DataContext != null && f.DialogResult == true)
                DataModel.Investors.Add(f.DataContext as Investor);
        }

        private void Button_DeleteInvestor_Click(object sender, RoutedEventArgs e)
        {
            DataModel.Investors.Remove(dataGrid.SelectedItem as Investor);
        }

        private void Button_EditInvestor_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                AddInvestor f = new AddInvestor();
                f.DataContext = (dataGrid.SelectedItem as Investor);
                f.Owner = this;
                f.ShowDialog();
            }
            else System.Windows.Forms.MessageBox.Show("Выберите инвестора для редактирования.");
        }
    }
}

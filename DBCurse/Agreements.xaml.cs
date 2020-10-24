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
    /// Логика взаимодействия для Agreements.xaml
    /// </summary>
    public partial class Agreements : Window
    {
        public Agreements()
        {
            InitializeComponent();
            Loaded += Agreements_Loaded;
        }

        private void Agreements_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new AgreementsViewModel();
        }

        private void Button_AddAgreement_Click(object sender, RoutedEventArgs e)
        {
            AddAgreement f = new AddAgreement();
            f.DataContext = new Agreement();
            f.Owner = this;
            f.ShowDialog();
            if (f.DataContext != null && f.DialogResult == true)
            {
                string error = "";
                if ((f.DataContext as Agreement).ClientName == null)
                    error += "Не был введен заказчик.\n";
                if (error == null)
                    System.Windows.Forms.MessageBox.Show(error, "Ошибка!");
                else
                {
                    DataModel.Agreements.Add(f.DataContext as Agreement);
                    DataModel.Operations.Add(new Operation(DataModel.Operations.Count, "Доход", (f.DataContext as Agreement).Amount, (f.DataContext as Agreement).GetDate));
                }
            }
        }

        private void Button_DeleteAgreement_Click(object sender, RoutedEventArgs e)
        {
            Operation operationToDelete = new Operation();
            foreach (Operation o in DataModel.Operations)
            {
                if (o.Amount == (dataGrid.SelectedItem as Agreement).Amount && o.OperationName == "Доход" && o.Date.Equals((dataGrid.SelectedItem as Agreement).Date))
                    operationToDelete = o;
            }
            DataModel.Operations.Remove(operationToDelete);
            DataModel.Agreements.Remove(dataGrid.SelectedItem as Agreement);
        }

        private void Button_EditAgreement_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                AddAgreement f = new AddAgreement();
                f.DataContext = (dataGrid.SelectedItem as Agreement);
                f.Owner = this;
                Operation oper = new Operation();
                foreach (Operation o in DataModel.Operations)
                {
                    if (o.Amount == (dataGrid.SelectedItem as Agreement).Amount && o.OperationName == "Доход" && o.Date.Equals((dataGrid.SelectedItem as Agreement).Date))
                    {
                        oper = o;
                    }
                }
                f.ShowDialog();

                if (f.DataContext != null && f.DialogResult == true)
                {
                    DataModel.Operations.Remove(oper);
                    DataModel.Operations.Add(new Operation(DataModel.Operations.Count, "Доход", (f.DataContext as Agreement).Amount, (f.DataContext as Agreement).GetDate));
                }
            }
            else System.Windows.Forms.MessageBox.Show("Выберите договор для редактирования.");
        }
    }
}

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
    /// Логика взаимодействия для Workers.xaml
    /// </summary>
    public partial class Workers : Window
    {
        public Workers()
        {
            InitializeComponent();
            Loaded += Workers_Loaded;
        }

        private void Workers_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new WorkersViewModel();
        }

        private void Button_AddWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorker f = new AddWorker();
            f.DataContext = new Worker();
            f.Owner = this;
            f.ShowDialog();
            if (f.DataContext != null && f.DialogResult == true)
            {
                string error = "";
                if ((f.DataContext as Worker).Telephone.ToString().Length != 6 || (f.DataContext as Worker).Telephone.ToString().Length != 11)
                    error += "Некорректно введен номер телефона.\n";
                if ((f.DataContext as Worker).PassportNumber == 0)
                    error += "Не был введен номер паспорта сотрудника.\n";
                if ((f.DataContext as Worker).PositionName == null)
                    error += "Не была введена должность.\n";
                if (error == null)
                    System.Windows.Forms.MessageBox.Show(error, "Ошибка!");
                else
                    DataModel.Workers.Add(f.DataContext as Worker);
            }

        }

        private void Button_DeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            DataModel.Workers.Remove(dataGrid.SelectedItem as Worker);
        }

        private void Button_EditWorker_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                AddWorker f = new AddWorker();
                f.DataContext = (dataGrid.SelectedItem as Worker);
                f.Owner = this;
                f.ShowDialog();
            }
            else System.Windows.Forms.MessageBox.Show("Выберите сотрудника для удаления.");
        }

        private void button_ShowProjects_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                WorkerParticipation f = new WorkerParticipation();
                f.DataContext = (dataGrid.SelectedItem as Worker);
                f.Owner = this;
                f.ShowDialog();
            }
            else System.Windows.Forms.MessageBox.Show("Выберите сотрудника.");
        }
    }
}

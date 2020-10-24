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
    /// Логика взаимодействия для EditCompanyStructure.xaml
    /// </summary>
    public partial class EditCompanyStructure : Window
    {
        public EditCompanyStructure()
        {
            InitializeComponent();
        }

        private void button_DeleteWorkerFromDepartment_Click(object sender, RoutedEventArgs e)
        {
            DataModel.WorkersInDepartments.Remove(GetWorkersInDepartment((int)(dataGrid_WorkersInDepartments.SelectedItem as Worker).Id, (int)(dataGrid_Departments.SelectedItem as Department).Id));
            (dataGrid_Departments.SelectedItem as Department).WorkersCollection.Remove(dataGrid_WorkersInDepartments.SelectedItem as Worker);
        }

        private void button_AddWorkerToDepartment_Click(object sender, RoutedEventArgs e)
        {
            (dataGrid_Departments.SelectedItem as Department).WorkersCollection.Add(dataGrid_Workers.SelectedItem as Worker);
            DataModel.WorkersInDepartments.Add(new WorkersInDepartment(DataModel.WorkersInDepartments.Count, (int)(dataGrid_Workers.SelectedItem as Worker).Id, (int)(dataGrid_Departments.SelectedItem as Department).Id));
        }

        private void button_AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            AddDepartment f = new AddDepartment();
            f.DataContext = new Department();
            f.Owner = this;
            f.ShowDialog();
            if (f.DialogResult == true && f.DataContext != null)
            {
                DataModel.Departments.Add(f.DataContext as Department);
            }
        }

        private void button_DeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            DataModel.Departments.Remove(dataGrid_Departments.SelectedItem as Department);
        }

        private void button_EditDepartment_Click(object sender, RoutedEventArgs e)
        {
            AddDepartment f = new AddDepartment();
            f.DataContext = (dataGrid_Departments.SelectedItem as Department);
            f.Owner = this;
            f.ShowDialog();
        }
        private WorkersInDepartment GetWorkersInDepartment(int idWorker, int idDepartment)
        {
            foreach (WorkersInDepartment wid in DataModel.WorkersInDepartments)
            {
                if (wid.IDWorker == idWorker && wid.IDDepartment == idDepartment)
                    return wid;
            }
            return null;
        }
    }
}

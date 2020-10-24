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
using DBCurse.Views;

namespace DBCurse
{
    /// <summary>
    /// Логика взаимодействия для EditProjectParticipation.xaml
    /// </summary>
    public partial class EditProjectParticipation : Window
    {
        public EditProjectParticipation()
        {
            InitializeComponent();
            Loaded += EditProjectParticipation_Loaded;
        }

        private void EditProjectParticipation_Loaded(object sender, RoutedEventArgs e)
        {
            WorkersViewModel wvm = new WorkersViewModel();
            ProjectsViewModel pvm = new ProjectsViewModel();
            dataGrid_Workers.DataContext = wvm;
            textBox_Workers.DataContext = wvm;
            dataGrid_Projects.DataContext = pvm;
            textBox_Projects.DataContext = pvm;
            System.Windows.Forms.MessageBox.Show("Не рекомендуется вносить изменения. Данная система автоматически распределяет сотрудников наиболее эффективно.");
        }

        private void button_AddWorkerToDepartment_Click(object sender, RoutedEventArgs e)
        {
            (dataGrid_Workers.SelectedItem as Worker).ProjectsCollection.Add(dataGrid_Projects.SelectedItem as Project);
            (dataGrid_Projects.SelectedItem as Project).WorkersCollection.Add(dataGrid_Workers.SelectedItem as Worker);
            DataModel.ProjectParticipations.Add(new ProjectParticipation(DataModel.ProjectParticipations.Count, (int)(dataGrid_Projects.SelectedItem as Project).Id, (int)(dataGrid_Workers.SelectedItem as Worker).Id));
        }

        private void button_DeleteWorkerFromDepartment_Click(object sender, RoutedEventArgs e)
        {
            (dataGrid_WorkersInDepartments.SelectedItem as Worker).ProjectsCollection.Remove(dataGrid_Projects.SelectedItem as Project);
            DataModel.ProjectParticipations.Remove(GetWorkersInProject((int)(dataGrid_Projects.SelectedItem as Project).Id, (int)(dataGrid_WorkersInDepartments.SelectedItem as Worker).Id));
            (dataGrid_Projects.SelectedItem as Project).WorkersCollection.Remove(dataGrid_WorkersInDepartments.SelectedItem as Worker);
        }
        private ProjectParticipation GetWorkersInProject(int idProject, int idWorker)
        {
            foreach (ProjectParticipation pp in DataModel.ProjectParticipations)
            {
                if (pp.IDProject == idProject && pp.IDWorker == idWorker)
                    return pp;
            }
            return null;
        }
    }
}

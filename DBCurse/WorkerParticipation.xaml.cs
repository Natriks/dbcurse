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
using DBCurse.Views;
using DBCurse.Models;

namespace DBCurse
{
    /// <summary>
    /// Логика взаимодействия для WorkerParticipation.xaml
    /// </summary>
    public partial class WorkerParticipation : Window
    {
        public WorkerParticipation()
        {
            InitializeComponent();
            Loaded += WorkerParticipation_Loaded;
        }

        private void WorkerParticipation_Loaded(object sender, RoutedEventArgs e)
        {
            //dataGrid_WorkerProjects.DataContext = this.DataContext;
            ProjectsViewModel pvm = new ProjectsViewModel();
            dataGrid_Projects.DataContext = pvm;
            textBox_Projects.DataContext = pvm;
        }

        private void button_AddProjectToWorker_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as Worker).ProjectsCollection.Add(dataGrid_Projects.SelectedItem as Project);
            (dataGrid_Projects.SelectedItem as Project).WorkersCollection.Add(this.DataContext as Worker);
            DataModel.ProjectParticipations.Add(new ProjectParticipation(DataModel.ProjectParticipations.Count, (int)(dataGrid_Projects.SelectedItem as Project).Id, (int)(this.DataContext as Worker).Id));
        }

        private void button_RemoveProjectFromWorker_Click(object sender, RoutedEventArgs e)
        {
            (dataGrid_WorkerProjects.SelectedItem as Project).WorkersCollection.Remove(this.DataContext as Worker);
            DataModel.ProjectParticipations.Remove(GetWorkersInProject((int)(dataGrid_WorkerProjects.SelectedItem as Project).Id, (int)(this.DataContext as Worker).Id));
            (this.DataContext as Worker).ProjectsCollection.Remove(dataGrid_WorkerProjects.SelectedItem as Project);
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

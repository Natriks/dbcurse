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
    /// Логика взаимодействия для ProjParticipation.xaml
    /// </summary>
    public partial class ProjParticipation : Window
    {
        public ProjParticipation()
        {
            InitializeComponent();
            Loaded += ProjParticipation_Loaded;
        }

        private void ProjParticipation_Loaded(object sender, RoutedEventArgs e)
        {
            WorkersViewModel wvm = new WorkersViewModel();
            dataGrid_Workers.DataContext = wvm;
            textBox_Workers.DataContext = wvm;
        }

        private void button_AddWorkerToProject_Click(object sender, RoutedEventArgs e)
        {
            //Добавление сотрудника в коллекцию сотрудников проекта
            (this.DataContext as Project).WorkersCollection.Add(dataGrid_Workers.SelectedItem as Worker);

            //Добавление проекта в коллекцию проектов сотрудника
            (dataGrid_Workers.SelectedItem as Worker).ProjectsCollection.Add(this.DataContext as Project);

            //Добавление связи проект-сотрудник
            DataModel.ProjectParticipations.Add(new ProjectParticipation(DataModel.ProjectParticipations.Count, (int)(this.DataContext as Project).Id, (int)(dataGrid_Workers.SelectedItem as Worker).Id));
        }
        
        private void button_RemoveWorkerFromProject_Click(object sender, RoutedEventArgs e)
        {
            //Удаление проекта из коллекции проектов сотрудника
            (dataGrid_WorkersInProject.SelectedItem as Worker).ProjectsCollection.Remove(this.DataContext as Project);

            //Удаление связи проект-сотрудник
            DataModel.ProjectParticipations.Remove(GetWorkersInProject((int)(this.DataContext as Project).Id, (int)(dataGrid_WorkersInProject.SelectedItem as Worker).Id));

            //Удаление сотрудника из коллекции сотрудников проекта
            (this.DataContext as Project).WorkersCollection.Remove(dataGrid_WorkersInProject.SelectedItem as Worker);
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

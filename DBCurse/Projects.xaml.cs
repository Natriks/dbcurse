using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using DBCurse.Models;
using DBCurse.Views;
using DBCurse.ProductiveModel;
using System.Collections.ObjectModel;

namespace DBCurse
{
    /// <summary>
    /// Логика взаимодействия для Projects.xaml
    /// </summary>
    public partial class Projects : Window
    {
        public Projects()
        {
            InitializeComponent();
            Loaded += Projects_Loaded;
            Closed += Projects_Closed;
        }

        private void Projects_Closed(object sender, EventArgs e)
        {
            Owner.DataContext = new ActiveProjectsViewModel();
            Owner.Focus();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                ProjectStages ProjectStagesDialog = new ProjectStages(dataGrid.SelectedItem as Project);
                ProjectStagesDialog.DataContext = (dataGrid.SelectedItem as Project);
                ProjectStagesDialog.Owner = this;
                ProjectStagesDialog.ShowDialog();
            }
            else System.Windows.Forms.MessageBox.Show("Выберите проект");
        }
        private void Projects_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new ProjectsViewModel();
        }
        private void Button_AddProject_Click(object sender, RoutedEventArgs e)
        {
            AddProject f = new AddProject();
            f.DataContext = new Project();
            f.Owner = this;
            f.ShowDialog();
            if (f.DataContext != null && f.DialogResult == true)
            {
                string error = null;
                if ((f.DataContext as Project).TypeName == null)
                    error += "Не был введен тип договора.\n";
                if ((f.DataContext as Project).ResponsibleName == null)
                    error += "Не был назначен ответственный за ведение проекта.\n";
                if ((f.DataContext as Project).AgreementID == 0)
                    error += "Не был введен номер договора.\n";
                if (error != null)
                    System.Windows.Forms.MessageBox.Show(error, "Ошибка!");
                else
                {
                    DataModel.Projects.Add(f.DataContext as Project);
                    DataModel.Projects.Last().StagesCollection = DataModel.Projects.First().StagesCollection;
                    foreach (string position in DataModel.PositionsCollection.ToArray())
                    {
                        ProductiveSolver ps = new ProductiveSolver((f.DataContext as Project), position);
                        Worker w = ps.ChooseWorker();
                        if (w != null)
                        {
                            DataModel.Projects.Last().WorkersCollection.Add(w);
                            DataModel.ProjectParticipations.Add(new ProjectParticipation(DataModel.ProjectParticipations.Count, (int)DataModel.Projects.Last().Id, (int)w.Id));
                            DataModel.PositionsCollection.Remove(position);
                        }
                        else
                            System.Windows.Forms.MessageBox.Show($"Работников на должности {position}, подходящих под правила продукционной модели не найдено.");
                    }
                    DataModel.PositionsCollection.Clear();
                }
            }
        }

        private void Button_DeleteProject_Click(object sender, RoutedEventArgs e)
        {
            if ((dataGrid.SelectedItem as Project) != null)
            {
                foreach (ProjectParticipation pp in DataModel.ProjectParticipations.ToArray())
                {
                    if (pp.IDProject == (int)(dataGrid.SelectedItem as Project).Id) DataModel.ProjectParticipations.Remove(pp);
                }
                DataModel.Projects.Remove(dataGrid.SelectedItem as Project);
            }
            else System.Windows.Forms.MessageBox.Show("Выберите проект для удаления.");
        }

        private void Button_EditProject_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                AddProject f = new AddProject();
                f.DataContext = (dataGrid.SelectedItem as Project);
                f.Owner = this;
                f.ShowDialog();
            }
            else System.Windows.Forms.MessageBox.Show("Выберите проект для редактирования.");
        }

        private void button_ProjPart_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                ProjParticipation ProjParticipationDialog = new ProjParticipation();
                ProjParticipationDialog.DataContext = (dataGrid.SelectedItem as Project);
                ProjParticipationDialog.Owner = this;
                ProjParticipationDialog.ShowDialog();
            }
            else System.Windows.Forms.MessageBox.Show("Выберите проект");
        }
    }
}

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
using System.Collections.ObjectModel;

namespace DBCurse
{
    /// <summary>
    /// Логика взаимодействия для AddProject.xaml
    /// </summary>
    public partial class AddProject : Window
    {
        public AddProject()
        {
            InitializeComponent();
            Loaded += AddProject_Loaded;
            Closed += AddProject_Closed;
        }

        private void AddProject_Closed(object sender, EventArgs e)
        {
            DataModel.PositionsCollection = new ObservableCollection<string>();
            foreach (string str in listBox.Items)
            DataModel.PositionsCollection.Add(str);
        }

        private void AddProject_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Worker w in DataModel.Workers)
            {
                comboBox_Responcible.Items.Add(w.Surname);
            }
            foreach (Agreement a in DataModel.Agreements)
            {
                comboBox_IDAgreement.Items.Add(a.Id);
            }
            foreach (ProjectType t in DataModel.ProjectTypes)
            {
                comboBox_ProjectType.Items.Add(t.Name);
            }
            comboBox_ProjectType.SelectedItem = (DataContext as Project).TypeName;
            foreach (Position p in DataModel.Positions)
            {
                comboBox_ProjectType_Copy.Items.Add(p.Name);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void button1_AddAgreement_Click(object sender, RoutedEventArgs e)
        {
            AddAgreement f = new AddAgreement();
            f.DataContext = new Agreement();
            f.Owner = this;
            f.ShowDialog();
            if (f.DataContext != null && f.DialogResult == true)
                DataModel.Agreements.Add(f.DataContext as Agreement);
        }

        private void button1_AddWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorker f = new AddWorker();
            f.DataContext = new Worker();
            f.Owner = this;
            f.ShowDialog();
            if (f.DataContext != null && f.DialogResult == true)
                DataModel.Workers.Add(f.DataContext as Worker);
        }

        private void button1_AddProjectType_Copy_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Add(comboBox_ProjectType_Copy.SelectedItem);
        }

        private void button1_DeletePosition_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Remove(listBox.SelectedItem);
        }
    }
}

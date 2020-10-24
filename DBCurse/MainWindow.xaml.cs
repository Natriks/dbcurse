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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using DBCurse.Models;
using DBCurse.Views;

namespace DBCurse
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new ActiveProjectsViewModel();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Workers formWorkers = new Workers();
            formWorkers.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Clients formClients = new Clients();
            formClients.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Agreements formAgreements = new Agreements();
            formAgreements.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Projects formProjects = new Projects();
            formProjects.Owner = this;
            formProjects.Show();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Investors formInvestors = new Investors();
            formInvestors.Show();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            Operations formOperations = new Operations();
            formOperations.Show();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            ProductiveRules f = new ProductiveRules();
            f.Show();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            EditCompanyStructure f = new EditCompanyStructure();
            f.DataContext = new WorkersViewModel();
            f.Show();
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            EditProjectParticipation f = new EditProjectParticipation();
            f.Show();
        }
    }
}

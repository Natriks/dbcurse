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
    /// Логика взаимодействия для EditStage.xaml
    /// </summary>
    public partial class EditStage : Window
    {
        public EditStage()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as Stage).StartDate = datePicker_StartDate.Text;
            (this.DataContext as Stage).EndDate = datePicker_EndDate.Text;
            this.DialogResult = true;
            this.Close();
        }
    }
}

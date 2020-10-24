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
using System.Windows.Forms.DataVisualization.Charting;

namespace DBCurse
{
    /// <summary>
    /// Логика взаимодействия для ProjectStages.xaml
    /// </summary>
    public partial class ProjectStages : Window
    {
        Project p;
        public ProjectStages(Project project)
        {
            InitializeComponent();
            p = project;
            Loaded += ProjectStages_Loaded;
        }

        private void ProjectStages_Loaded(object sender, RoutedEventArgs e)
        {
            DrawAxis();
        }
        public void DrawAxis()
        {
            chart.ChartAreas.Clear();
            chart.Series.Clear();

            ChartArea chartArea1 = new ChartArea();
            chart.ChartAreas.Add(chartArea1);

            Series series1 = new Series();
            Series series2 = new Series();
            series1.ChartType = SeriesChartType.StackedBar;
            series2.ChartType = SeriesChartType.StackedBar;
            series1.Color = System.Drawing.Color.Transparent;

            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.AxisX.ScaleView.Zoomable = true;
            chartArea1.AxisY.ScrollBar.IsPositionedInside = true;

            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.AxisY.ScaleView.Zoomable = true;
            chartArea1.AxisY.ScrollBar.IsPositionedInside = true;

            double d = 0;
            foreach (Stage s in p.StagesCollection.Reverse())
            {
                d = ConvertFromDateToDouble(s.StartDate);
            }
            foreach (Stage s in p.StagesCollection.Reverse())
            {
                series1.Points.Add(ConvertFromDateToDouble(s.StartDate) - d).AxisLabel = s.Name;
            }

            //series1.Points.Add(44);
            //series1.Points.Add(34);
            //series1.Points.Add(32);
            //series1.Points.Add(28);
            //series1.Points.Add(24);

            foreach (Stage s in p.StagesCollection.OrderBy(x => p.StartDate).Reverse())
            {
                series2.Points.Add(ConvertFromDateToDouble(s.EndDate) - ConvertFromDateToDouble(s.StartDate));
            }

            //series2.Points.Add(44);
            //series2.Points.Add(32);
            //series2.Points.Add(28);
            //series2.Points.Add(24);
            //series2.Points.Add(14);

            chart.Series.Add(series1);
            chart.Series.Add(series2);

            //chart.Location = new System.Drawing.Point(0, 0);
            //chart.Size = new System.Drawing.Size(360, 360);
            
        }
        double ConvertFromDateToDouble(string s)
        {
            double d = 0;
            int years = int.Parse(s[0].ToString() + s[1].ToString() + s[2].ToString() + s[3].ToString());
            int month = int.Parse(s[5].ToString() + s[6].ToString());
            int days = int.Parse(s[8].ToString() + s[9].ToString());
            int str;
            d = years - DateTime.Now.Year;
            if (d <= 0)
                str = 0;
            else
                str = (int)d * 365;
            str += month * 30;
            str += days;
            d = str;
            return d;
        }

        private void button_AddStage_Click(object sender, RoutedEventArgs e)
        {
            EditStage f = new EditStage();
            f.DataContext = new Stage();
            f.Owner = this;
            f.ShowDialog();
            if (f.DataContext != null && f.DialogResult == true)
            {
                (this.DataContext as Project).StagesCollection.Add(f.DataContext as Stage);
                DataModel.Stages.Add(f.DataContext as Stage);
                DataModel.ProjectStagesCollection.Add(new ProjectStage(DataModel.ProjectStagesCollection.Count, (int)(this.DataContext as Project).Id, (int)(f.DataContext as Stage).Id));
                DrawAxis();
            }
        }
        private void button_DeleteStage_Click(object sender, RoutedEventArgs e)
        {
            DataModel.ProjectStagesCollection.Remove(GetStagesInProject((int)(this.DataContext as Project).Id, (int)(listBox.SelectedItem as Stage).Id));
            (this.DataContext as Project).StagesCollection.Remove(listBox.SelectedItem as Stage);
            DrawAxis();
        }
        private ProjectStage GetStagesInProject(int idProject, int idStage)
        {
            foreach (ProjectStage ps in DataModel.ProjectStagesCollection)
            {
                if (ps.IDProject == idProject && ps.IDStage == idStage)
                    return ps;
            }
            return null;
        }
    }
}

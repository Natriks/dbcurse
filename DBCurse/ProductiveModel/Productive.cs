using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DBCurse.Models;

namespace DBCurse.ProductiveModel
{
    class Productive : INotifyPropertyChanged
    {

        private string projectUrgency, projectValue, workerBusy, workerPerformance, priority;
        public long Id { get; set; }
        public String ProjectUrgency
        {
            get { return projectUrgency; }
            set { projectUrgency = value; OnPropertyChanged("ProjectUrgency"); }
        }
        public String ProjectValue
        {
            get { return projectValue; }
            set { projectValue = value; OnPropertyChanged("ProjectValue"); }
        }
        public String WorkerBusy
        {
            get { return workerBusy; }
            set { workerBusy = value; OnPropertyChanged("WorkerBusy"); }
        }
        public String WorkerPerformance
        {
            get { return workerPerformance; }
            set { workerPerformance = value; OnPropertyChanged("WorkerPerformance"); }
        }
        public String Priority
        {
            get { return priority; }
            set { priority = value; OnPropertyChanged("Priority"); }
        }
        public Productive()
        {
            this.ProjectUrgency = "";
            this.ProjectValue = "";
            this.WorkerBusy = "";
            this.WorkerPerformance = "";
        }
        public Productive(long id, string projectUrgency, string projectValue, string workerBusy, string workerPerformance, string priority)
        {
            this.Id = id;
            this.ProjectUrgency = projectUrgency;
            this.ProjectValue = projectValue;
            this.WorkerBusy = workerBusy;
            this.WorkerPerformance = workerPerformance;
            this.Priority = priority;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

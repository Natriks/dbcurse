using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DBCurse.Models
{
    class ProjectStage : INotifyPropertyChanged
    {
        private int idProject, idStage;
        public long Id { get; set; }
        public Int32 IDProject
        {
            get { return idProject; }
            set { idProject = value; OnPropertyChanged("IDProject"); }
        }
        public Int32 IDStage
        {
            get { return idStage; }
            set { idStage = value; OnPropertyChanged("IDStage"); }
        }
        public ProjectStage()
        {
            this.IDProject = 1;
            this.IDStage = 1;
        }
        public ProjectStage(long id, int idProject, int idStage)
        {
            this.Id = id;
            this.IDProject = idProject;
            this.IDStage = idStage;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

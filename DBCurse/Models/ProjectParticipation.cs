using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DBCurse.Models
{
    class ProjectParticipation : INotifyPropertyChanged
    {
        private int idProject, idWorker;
        public long Id { get; set; }
        public Int32 IDProject
        {
            get { return idProject; }
            set { idProject = value; OnPropertyChanged("IDProject"); }
        }
        public Int32 IDWorker
        {
            get { return idWorker; }
            set { idWorker = value; OnPropertyChanged("IDWorker"); }
        }
        public ProjectParticipation()
        {
            this.IDProject = 1;
            this.IDWorker = 1;
        }
        public ProjectParticipation(long id, int idProject, int idWorker)
        {
            this.Id = id;
            this.IDProject = idProject;
            this.IDWorker = idWorker;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

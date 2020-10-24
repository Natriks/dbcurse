using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DBCurse.Models
{
    class WorkersInDepartment:INotifyPropertyChanged
    {
        private int idDepartment, idWorker;
        public long Id { get; set; }
        public Int32 IDWorker
        {
            get { return idWorker; }
            set { idWorker = value; OnPropertyChanged("IDWorker"); }
        }
        public Int32 IDDepartment
        {
            get { return idDepartment; }
            set { idDepartment = value; OnPropertyChanged("IDDepartment"); }
        }
        public WorkersInDepartment()
        {
            this.IDWorker = 1;
            this.IDDepartment = 1;
        }
        public WorkersInDepartment(long id, int idWorker, int idDepartment)
        {
            this.Id = id;
            this.IDWorker = idWorker;
            this.IDDepartment = idDepartment;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

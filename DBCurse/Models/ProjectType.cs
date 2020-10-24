using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DBCurse.Models
{
    class ProjectType : INotifyPropertyChanged
    {
        private string name;
        public long Id { get; set; }
        public String Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        public ProjectType()
        {
            this.Name = "";
        }
        public ProjectType(long id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

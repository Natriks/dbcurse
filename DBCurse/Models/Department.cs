using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace DBCurse.Models
{
    public class Department : INotifyPropertyChanged
    {
        private string name, leaderName;
        private ObservableCollection<Worker> workersCollection;
        public long Id { get; set; }
        public String Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        public String LeaderName
        {
            get { return leaderName; }
            set { leaderName = value; OnPropertyChanged("LeaderName"); }
        }
        public ObservableCollection<Worker> WorkersCollection
        {
            get { return workersCollection; }
            set { workersCollection = value; OnPropertyChanged("WorkersCollection"); }
        }
        public Department()
        {
            this.Name = "";
            this.LeaderName = "";
            this.WorkersCollection = new ObservableCollection<Worker>();
        }
        public Department(long id, string name, string leaderName)
        {
            this.Id = id;
            this.Name = name;
            this.LeaderName = leaderName;
            this.WorkersCollection = new ObservableCollection<Worker>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

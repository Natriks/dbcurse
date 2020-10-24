using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace DBCurse.Models
{
    public class Stage : INotifyPropertyChanged
    {
        private string name;
        private DateTime startDate, endDate;
        public long Id { get; set; }
        public String Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        public String StartDate
        {
            get { return startDate.ToString("yyyy-MM-dd HH:mm:ss"); }
            set
            {
                startDate = Convert.ToDateTime(value);
                OnPropertyChanged("StartDate");
            }
        }
        public String EndDate
        {
            get { return endDate.ToString("yyyy-MM-dd HH:mm:ss"); }
            set
            {
                endDate = Convert.ToDateTime(value);
                OnPropertyChanged("EndDate");
            }
        }
        public Stage()
        {
            this.Name = "";
            this.StartDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.EndDate = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd HH:mm:ss");
        }
        public Stage(long id, string name, DateTime startDate, DateTime endDate)
        {
            this.Id = id;
            this.Name = name;
            this.StartDate = startDate.ToString("yyyy-MM-dd HH:mm:ss");
            this.EndDate = endDate.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

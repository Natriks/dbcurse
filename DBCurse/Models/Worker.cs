using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace DBCurse.Models
{
    public class Worker : INotifyPropertyChanged , ICloneable
    {
        private string name, surname, patronomic, positionName, email, address;
        private int hourlyPayment, telephone, passportNumber, performance;
        private DateTime birthday;
        private ObservableCollection<Project> projectsCollection;
        public long Id { get; set; }
        public ObservableCollection<Project> ProjectsCollection
        {
            get { return projectsCollection; }
            set { projectsCollection = value; OnPropertyChanged("ProjectsCollection"); }
        }
        public String Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        public String Surname
        {
            get { return surname; }
            set { surname = value; OnPropertyChanged("Surname"); }
        }
        public String Patronomic
        {
            get { return patronomic; }
            set { patronomic = value; OnPropertyChanged("Patronomic"); }
        }
        public String Birthday
        {
            get { return birthday.ToString("yyyy-MM-dd HH:mm:ss"); }
            set
            {
                birthday = Convert.ToDateTime(value);
                OnPropertyChanged("Birthday");
            }
        }
        public String PositionName
        {
            get { return positionName; }
            set { positionName = value; OnPropertyChanged("PositionName"); }
        }
        public Int32 HourlyPayment
        {
            get { return hourlyPayment; }
            set { hourlyPayment = value; OnPropertyChanged("HourlyPayment"); }
        }
        public Int32 PassportNumber
        {
            get { return passportNumber; }
            set { passportNumber = value; OnPropertyChanged("PassportNumber"); }
        }
        public String Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }
        public Int32 Telephone
        {
            get { return telephone; }
            set { telephone = value; OnPropertyChanged("Telephone"); }
        }
        public String Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged("Address"); }
        }
        public Int32 Performance
        {
            get { return performance; }
            set { performance = value; OnPropertyChanged("Performance"); }
        }
        public Worker()
        {
            this.Name = "";
            this.Surname = "";
            this.Patronomic = "";
            this.Birthday = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.PositionName = "";
            this.HourlyPayment = 0;
            this.PassportNumber = 0;
            this.Email = "";
            this.Telephone = 0;
            this.Address = "";
            this.Performance = 1;
            ProjectsCollection = new ObservableCollection<Project>();
        }
        public Worker(long id, string name, string surname, string patronomic, DateTime birthday, string positionName, int hourlyPayment, 
            int passportNumber, string email, int telephone, string address, int performance)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.Patronomic = patronomic;
            this.Birthday = birthday.ToString("yyyy-MM-dd HH:mm:ss");
            this.PositionName = positionName;
            this.HourlyPayment = hourlyPayment;
            this.PassportNumber = passportNumber;
            this.Email = email;
            this.Telephone = telephone;
            this.Address = address;
            this.Performance = performance;
            ProjectsCollection = new ObservableCollection<Project>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public int GetProjectsPartisipationCount()
        {
            int count = 0;
            foreach (Project p in DataModel.Projects)
            {
                foreach (Worker wp in p.WorkersCollection)
                {
                    if (wp.Id == this.Id)
                        count++;
                }
            }
            return count;
        }
        public object Clone()
        {
            Worker w = new Worker();
            w.Address = this.Address;
            w.Birthday = this.Birthday;
            w.Email = this.Email;
            w.HourlyPayment = this.HourlyPayment;
            w.Id = this.Id;
            w.Name = this.Name;
            w.PositionName = this.PositionName;
            w.Surname = this.Surname;
            w.PassportNumber = this.PassportNumber;
            w.Telephone = this.Telephone;
            w.Performance = this.Performance;
            w.Patronomic = this.Patronomic;
            return w;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace DBCurse.Models
{
    public class Project : INotifyPropertyChanged
    {
        private string name, description, responsibleName, typeName;
        private int agreementID, priority;
        private DateTime startDate, endDate;
        private ObservableCollection<Worker> workersCollection;
        private ObservableCollection<Stage> stagesCollection;
        private ObservableCollection<string> positionsCollection;
        public long Id { get; set; }
        public ObservableCollection<Worker> WorkersCollection
        {
            get { return workersCollection; }
            set { workersCollection = value; OnPropertyChanged("WorkersCollection"); }
        }
        public ObservableCollection<Stage> StagesCollection
        {
            get { return stagesCollection; }
            set { stagesCollection = value; OnPropertyChanged("StagesCollection"); }
        }
        public Int32 AgreementID
        {
            get { return agreementID; }
            set { agreementID = value; OnPropertyChanged("AgreementID"); }
        }
        public Int32 Priority
        {
            get { return priority; }
            set { priority = value; OnPropertyChanged("Priority"); }
        }
        public String TypeName
        {
            get { return typeName; }
            set { typeName = value; OnPropertyChanged("TypeName"); }
        }
        public String Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        public String ResponsibleName
        {
            get { return responsibleName; }
            set { responsibleName = value; OnPropertyChanged("ResponsibleName"); }
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
        public DateTime GetEndDate
        {
            get { return endDate; }
            set
            {
                endDate = Convert.ToDateTime(value);
            }
        }
        public String Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }

        public Project()
        {
            this.Name = "";
            this.TypeName = "";
            this.AgreementID = 1;
            this.ResponsibleName = "";
            this.StartDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.EndDate = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm:ss");
            this.Description = "Отсутствует.";
            this.WorkersCollection = new ObservableCollection<Worker>();
            this.StagesCollection = new ObservableCollection<Stage>();
        }

        public Project(long id, string name, string typeName, int agreementID, string responsibleName, DateTime startDate, DateTime endDate, string description)
        {
            this.Id = id;
            this.Name = name;
            this.TypeName = typeName;
            this.AgreementID = agreementID;
            this.ResponsibleName = responsibleName;
            this.StartDate = startDate.ToString("yyyy-MM-dd HH:mm:ss");
            this.EndDate = endDate.ToString("yyyy-MM-dd HH:mm:ss");
            this.Description = description;
            this.WorkersCollection = new ObservableCollection<Worker>();
            this.StagesCollection = new ObservableCollection<Stage>();
        }
        /// <summary>
        /// Метод подготовлен для ЭС, использование нежелательно
        /// </summary>
        /// <returns></returns>
        public double GetPriority()
        {
            int priority = 0;
            int count = 0;
            foreach (Worker w in this.WorkersCollection)
            {
                count++;
            }
            priority = GetAgreement().Amount;
            return priority;
        }
        public Agreement GetAgreement()
        {
            foreach (Agreement a in DataModel.Agreements)
            {
                if (a.Id == this.AgreementID)
                    return a;
            }
            return null;
        }
        public int GetUrgency()
        {
            if (EndDateIsPast())
                return 0;
            return (int)ConvertFromDateToDouble(this.EndDate) - (int)ConvertFromDateToDouble(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        private double ConvertFromDateToDouble(string s)
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
        public bool EndDateIsPast()
        {
            string date = this.EndDate;
            int years = int.Parse(date[0].ToString() + date[1].ToString() + date[2].ToString() + date[3].ToString());
            int month = int.Parse(date[5].ToString() + date[6].ToString());
            int days = int.Parse(date[8].ToString() + date[9].ToString());
            if (years < DateTime.Now.Year)
                return true;
            if (years == DateTime.Now.Year)
                if (month < DateTime.Now.Month)
                    return true;
            if (years == DateTime.Now.Year)
                if (month == DateTime.Now.Month)
                    if (days < DateTime.Now.Day)
                        return true;
            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

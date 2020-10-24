using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DBCurse.Models
{
    public class Agreement : INotifyPropertyChanged
    {
        private string clientName, description;
        private DateTime date, deadline;
        private int amount;
        public long Id { get; set; }
        public String ClientName
        {
            get { return clientName; }
            set { clientName = value; OnPropertyChanged("ClientName"); }
        }
        public String Date
        {
            get { return date.ToString("yyyy-MM-dd HH:mm:ss"); }
            set
            {
                date = Convert.ToDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        public DateTime GetDate
        {
            get { return date; }
            set
            {
                date = Convert.ToDateTime(value);
            }
        }
        public String Deadline
        {
            get { return deadline.ToString("yyyy-MM-dd HH:mm:ss"); }
            set
            {
                deadline = Convert.ToDateTime(value);
                OnPropertyChanged("Deadline");
            }
        }
        public Int32 Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged("Amount"); }
        }
        public String Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }
        public Agreement()
        {
            this.ClientName = "";
            this.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.Deadline = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm:ss");
            this.Amount = 0;
            this.Description = "Отсутствует.";
        }

        public Agreement(long id, string clientName, DateTime date, DateTime deadline, int amount, string description)
        {
            this.Id = id;
            this.ClientName = clientName;
            this.Date = date.ToString("yyyy-MM-dd HH:mm:ss");
            this.Deadline = deadline.ToString("yyyy-MM-dd HH:mm:ss");
            this.Amount = amount;
            this.Description = description;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

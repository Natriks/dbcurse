using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DBCurse.Models
{
    public class Operation : INotifyPropertyChanged
    {
        private string operationName;
        int amount;
        DateTime date;
        public long Id { get; set; }
        public String OperationName
        {
            get { return operationName; }
            set { operationName = value; OnPropertyChanged("OperationName"); }
        }
        public Int32 Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged("Amount"); }
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
        public Operation()
        {
            this.OperationName = "";
            this.Amount = 0;
            this.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public Operation(long id, string operationName, int amount, DateTime date)
        {
            this.Id = id;
            this.OperationName = operationName;
            this.Amount = amount;
            this.Date = date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

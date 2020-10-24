using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DBCurse.Models
{
    public class Position : INotifyPropertyChanged
    {
        private string name, payment;
        public long Id { get; set; }
        public String Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        public String Payment
        {
            get { return payment; }
            set { payment = value; OnPropertyChanged("Payment"); }
        }
        public Position()
        {
            this.Name = "";
        }
        public Position(long id, string name, string payment)
        {
            this.Id = id;
            this.Name = name;
            this.Payment = payment;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

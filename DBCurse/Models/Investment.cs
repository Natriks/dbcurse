using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DBCurse.Models
{
    public class Investment : INotifyPropertyChanged
    {
        private string nameInvestor;
        int amount;
        public long Id { get; set; }
        public String NameInvestor
        {
            get { return nameInvestor; }
            set { nameInvestor = value; OnPropertyChanged("NameInvestor"); }
        }
        public Int32 Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged("Amount"); }
        }

        public Investment()
        {
            this.NameInvestor = "";
            this.Amount = 0;
        }
        public Investment(long id, string nameInvestor, int amount)
        {
            this.Id = id;
            this.NameInvestor = nameInvestor;
            this.Amount = amount;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

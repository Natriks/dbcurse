using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCurse.Models;
using System.ComponentModel;

namespace DBCurse.ProductiveModel
{
    class ProductAttributeValue : INotifyPropertyChanged
    {
        private string nameValue;
        private int max, min, idAttribute;
        public long Id { get; set; }
        public String NameValue
        {
            get { return nameValue; }
            set { nameValue = value; OnPropertyChanged("NameValue"); }
        }
        public Int32 IDAttribute
        {
            get { return idAttribute; }
            set { idAttribute = value; OnPropertyChanged("IDAttribute"); }
        }
        public Int32 Min
        {
            get { return min; }
            set { min = value; OnPropertyChanged("Min"); }
        }
        public Int32 Max
        {
            get { return max; }
            set { max = value; OnPropertyChanged("Max"); }
        }
        public ProductAttributeValue()
        {
            this.NameValue = "";
            this.Min = 0;
            this.Max = 0;
        }
        public ProductAttributeValue(long id, int idAttribute, string nameValue, int min, int max)
        {
            this.Id = id;
            this.IDAttribute = idAttribute;
            this.NameValue = nameValue;
            this.Min = min;
            this.Max = max;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
